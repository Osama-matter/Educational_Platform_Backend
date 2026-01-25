using Educational_Platform_Front_End.Models.Enrollment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                var enrollmentsResponse = await client.GetAsync("https://localhost:7228/api/enrollments");
                if (!enrollmentsResponse.IsSuccessStatusCode) { Enrollments = new List<EnrollmentViewModel>(); return Page(); }
                var enrollmentsContent = await enrollmentsResponse.Content.ReadAsStringAsync();
                var enrollments = JsonSerializer.Deserialize<List<EnrollmentViewModel>>(enrollmentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var coursesResponse = await client.GetAsync("https://localhost:7228/api/courses");
                if (!coursesResponse.IsSuccessStatusCode) { Enrollments = new List<EnrollmentViewModel>(); return Page(); }
                var coursesContent = await coursesResponse.Content.ReadAsStringAsync();
                var courses = JsonSerializer.Deserialize<List<Models.Courses.CourseViewModel>>(coursesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var lessonsResponse = await client.GetAsync("https://localhost:7228/api/lessons");
                if (!lessonsResponse.IsSuccessStatusCode) { Enrollments = new List<EnrollmentViewModel>(); return Page(); }
                var lessonsContent = await lessonsResponse.Content.ReadAsStringAsync();
                var allLessons = JsonSerializer.Deserialize<List<Models.Lessons.LessonViewModel>>(lessonsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var progressResponse = await client.GetAsync("https://localhost:7228/api/progress");
                if (!progressResponse.IsSuccessStatusCode) { Enrollments = new List<EnrollmentViewModel>(); return Page(); }
                var progressContent = await progressResponse.Content.ReadAsStringAsync();
                var progress = JsonSerializer.Deserialize<List<Models.Progress.ProgressViewModel>>(progressContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var enrollment in enrollments)
                {
                    enrollment.Course = courses.FirstOrDefault(c => c.Id == enrollment.CourseId);
                    if (enrollment.Course != null)
                    {
                        var courseLessons = allLessons.Where(l => l.CourseId == enrollment.CourseId).ToList();
                        var completedLessons = progress.Count(p => p.EnrollmentId == enrollment.Id && courseLessons.Any(l => l.Id == p.LessonId));
                        enrollment.Progress = courseLessons.Any() ? (double)completedLessons / courseLessons.Count : 0;
                    }
                }

                Enrollments = enrollments.Where(e => e.Course != null).ToList();
            }

            return Page();
        }
    }
}
