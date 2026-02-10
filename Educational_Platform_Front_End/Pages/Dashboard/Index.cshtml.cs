using Educational_Platform_Front_End.Models.Enrollment;
using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Educational_Platform_Front_End.Models.Progress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; } = new List<EnrollmentViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.FindFirstValue("nameid");
            }

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:7228/");

                // 1. Get ALL enrollments for the student
                var enrollmentsResponse = await client.GetAsync("api/enrollments");
                if (!enrollmentsResponse.IsSuccessStatusCode)
                {
                    Enrollments = new List<EnrollmentViewModel>();
                    return Page();
                }
                var enrollmentsContent = await enrollmentsResponse.Content.ReadAsStringAsync();
                var allEnrollments = JsonSerializer.Deserialize<List<EnrollmentViewModel>>(enrollmentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Filter enrollments for the current student and active/paid status
                var studentEnrollments = allEnrollments.Where(e => 
                    e.StudentId.ToString().ToLower() == userId.ToLower() && 
                    (e.IsActive || e.PaymentStatus == "Paid")).ToList();

                if (!studentEnrollments.Any())
                {
                    Enrollments = new List<EnrollmentViewModel>();
                    return Page();
                }

                // 2. Get Courses
                var coursesResponse = await client.GetAsync("api/courses");
                var coursesContent = await coursesResponse.Content.ReadAsStringAsync();
                var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(coursesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // 3. Get Lessons
                var lessonsResponse = await client.GetAsync("api/lessons");
                var lessonsContent = await lessonsResponse.Content.ReadAsStringAsync();
                var allLessons = JsonSerializer.Deserialize<List<LessonViewModel>>(lessonsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // 4. Get Progress
                var progressResponse = await client.GetAsync("api/progress");
                var progressContent = await progressResponse.Content.ReadAsStringAsync();
                var progress = JsonSerializer.Deserialize<List<ProgressViewModel>>(progressContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var enrollment in studentEnrollments)
                {
                    enrollment.Course = courses.FirstOrDefault(c => c.Id == enrollment.CourseId);
                    if (enrollment.Course != null)
                    {
                        var courseLessons = allLessons.Where(l => l.CourseId == enrollment.CourseId).ToList();
                        var completedLessons = progress.Count(p => p.EnrollmentId == enrollment.Id && courseLessons.Any(l => l.Id == p.LessonId));
                        enrollment.Progress = courseLessons.Any() ? (double)completedLessons / courseLessons.Count : 0;
                    }
                }

                Enrollments = studentEnrollments.Where(e => e.Course != null).ToList();
            }

            return Page();
        }
    }
}
