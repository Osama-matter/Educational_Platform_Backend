using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        public CourseViewModel Course { get; set; }
        public IEnumerable<LessonViewModel> Lessons { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var courseResponse = await client.GetAsync($"https://localhost:7228/api/courses/{id}");

                if (!courseResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var courseContent = await courseResponse.Content.ReadAsStringAsync();
                Course = JsonSerializer.Deserialize<CourseViewModel>(courseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var lessonsResponse = await client.GetAsync("https://localhost:7228/api/lessons");
                if (lessonsResponse.IsSuccessStatusCode)
                {
                    var lessonsContent = await lessonsResponse.Content.ReadAsStringAsync();
                    var allLessons = JsonSerializer.Deserialize<IEnumerable<LessonViewModel>>(lessonsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    Lessons = allLessons.Where(l => l.CourseId == id).OrderBy(l => l.OrderIndex);
                }
                else
                {
                    Lessons = new List<LessonViewModel>();
                }

                return Page();
            }
        }
    }
}
