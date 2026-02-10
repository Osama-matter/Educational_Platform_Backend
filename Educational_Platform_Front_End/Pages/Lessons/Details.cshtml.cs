using Educational_Platform_Front_End.Models.Lessons;
using EducationalPlatform.Application.DTOs.Enrollments;
using EducationalPlatform.Application.DTOs.Progress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        public LessonViewModel Lesson { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToPage("/Account/Login");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                // 1. Get Lesson Details
                var response = await client.GetAsync($"https://localhost:7228/api/lessons/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        StatusMessage = "Access denied. You must be enrolled and have paid for the course to view this lesson.";
                        return RedirectToPage("/Courses/Index");
                    }
                    return NotFound();
                }

                var content = await response.Content.ReadAsStringAsync();
                Lesson = JsonSerializer.Deserialize<LessonViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCompleteAsync(Guid id)
        {
            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToPage("/Account/Login");
            }

            var userId = TryGetUserIdFromJwt(token);
            if (userId == null)
            {
                StatusMessage = "Unable to identify user.";
                return RedirectToPage(new { id });
            }

            LessonViewModel lesson;
            using (var lessonClient = new HttpClient())
            {
                var lessonResponse = await lessonClient.GetAsync($"https://localhost:7228/api/lessons/{id}");
                if (!lessonResponse.IsSuccessStatusCode)
                {
                    StatusMessage = "Lesson not found.";
                    return RedirectToPage(new { id });
                }

                var lessonJson = await lessonResponse.Content.ReadAsStringAsync();
                lesson = JsonSerializer.Deserialize<LessonViewModel>(lessonJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            if (lesson == null)
            {
                StatusMessage = "Lesson not found.";
                return RedirectToPage(new { id });
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                EnrollmentDto enrollment = null;
                var enrollmentsResponse = await client.GetAsync("https://localhost:7228/api/enrollments");
                if (enrollmentsResponse.IsSuccessStatusCode)
                {
                    var enrollmentsJson = await enrollmentsResponse.Content.ReadAsStringAsync();
                    var enrollments = JsonSerializer.Deserialize<List<EnrollmentDto>>(enrollmentsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<EnrollmentDto>();
                    enrollment = enrollments.FirstOrDefault(e => e.CourseId == lesson.CourseId && e.StudentId == userId.Value);
                }

                if (enrollment == null)
                {
                    var createEnrollmentResponse = await client.PostAsync($"https://localhost:7228/api/enrollments?courseId={lesson.CourseId}", null);
                    if (!createEnrollmentResponse.IsSuccessStatusCode)
                    {
                        StatusMessage = "You must be enrolled in this course before marking lessons complete.";
                        return RedirectToPage(new { id });
                    }

                    var createdEnrollmentJson = await createEnrollmentResponse.Content.ReadAsStringAsync();
                    enrollment = JsonSerializer.Deserialize<EnrollmentDto>(createdEnrollmentJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }

                if (enrollment == null)
                {
                    StatusMessage = "Unable to create enrollment.";
                    return RedirectToPage(new { id });
                }

                var progressPayload = new CreateLessonProgressDto
                {
                    EnrollmentId = enrollment.Id,
                    LessonId = lesson.Id
                };

                var content = new StringContent(JsonSerializer.Serialize(progressPayload), Encoding.UTF8, "application/json");
                var progressResponse = await client.PostAsync("https://localhost:7228/api/progress", content);

                if (!progressResponse.IsSuccessStatusCode)
                {
                    if ((int)progressResponse.StatusCode == 409)
                    {
                        StatusMessage = "Lesson already marked as complete.";
                        return RedirectToPage(new { id });
                    }

                    StatusMessage = "Could not mark lesson as complete.";
                    return RedirectToPage(new { id });
                }

                StatusMessage = "Lesson marked as complete.";
                return RedirectToPage(new { id });
            }
        }

        private static Guid? TryGetUserIdFromJwt(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var claimValue = jwt.Claims.FirstOrDefault(c =>
                        c.Type == ClaimTypes.NameIdentifier ||
                        c.Type.EndsWith("/nameidentifier", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Type, "nameid", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(c.Type, "sub", StringComparison.OrdinalIgnoreCase))
                    ?.Value;

                if (Guid.TryParse(claimValue, out var userId))
                {
                    return userId;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
