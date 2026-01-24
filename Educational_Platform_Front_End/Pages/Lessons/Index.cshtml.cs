using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Lessons
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public List<CourseSection> CoursesByType { get; set; } = new();
        public List<string> CourseTypes { get; set; } = new();
        public PlatformStats Stats { get; set; }
        public int? TotalCourses => CoursesByType?.Sum(s => s.Courses?.Count ?? 0);

        public class CourseSection
        {
            public string Type { get; set; }
            public List<CourseViewModel> Courses { get; set; }
        }

        public class PlatformStats
        {
            public int TotalCourses { get; set; }
            public int TotalInstructors { get; set; }
            public int TotalStudents { get; set; }
            public int CompletionRate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                using var client = new HttpClient();

                // Fetch courses from API
                var coursesResponse = await client.GetAsync("https://localhost:7228/api/courses");
                if (coursesResponse.IsSuccessStatusCode)
                {
                    var content = await coursesResponse.Content.ReadAsStringAsync();
                    var allCourses = JsonSerializer.Deserialize<List<CourseViewModel>>(
                        content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    // Group courses by type
                    var groupedCourses = allCourses?
                        .Where(c => c.IsActive)
                        .GroupBy(c => c.Description ?? "Uncategorized")
                        .Select(g => new CourseSection
                        {
                            Type = g.Key,
                            Courses = g.OrderBy(c => c.Title).ToList()
                        })
                        .OrderBy(s => s.Type)
                        .ToList();

                    CoursesByType = groupedCourses ?? new List<CourseSection>();
                    CourseTypes = CoursesByType.Select(s => s.Type).Distinct().ToList();
                }

                // Fetch platform stats
                var statsResponse = await client.GetAsync("https://localhost:7228/api/stats");
                if (statsResponse.IsSuccessStatusCode)
                {
                    var statsContent = await statsResponse.Content.ReadAsStringAsync();
                    Stats = JsonSerializer.Deserialize<PlatformStats>(
                        statsContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );
                }
                else
                {
                    // Generate default stats if API fails
                    Stats = new PlatformStats
                    {
                        TotalCourses = CoursesByType.Sum(s => s.Courses?.Count ?? 0),
                        TotalInstructors = CoursesByType.SelectMany(s => s.Courses)
                            .Select(c => c.Title)
                            .Distinct()
                            .Count(),
                        TotalStudents = 25000,
                        CompletionRate = 94
                    };
                }
            }
            catch
            {
                // Handle errors gracefully
                CoursesByType = new List<CourseSection>();
                Stats = new PlatformStats
                {
                    TotalCourses = 0,
                    TotalInstructors = 0,
                    TotalStudents = 0,
                    CompletionRate = 0
                };
            }

            return Page();
        }
    }
}