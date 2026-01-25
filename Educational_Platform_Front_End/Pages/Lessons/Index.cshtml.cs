using Educational_Platform_Front_End.Models.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
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
        public IEnumerable<LessonViewModel> Lessons { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] Guid? courseId)
        {
            if (courseId == null)
            {
                return RedirectToPage("/Dashboard/AdminDashboard");
            }

            using (var client = new HttpClient())
            {
                var lessonsResponse = await client.GetAsync("https://localhost:7228/api/lessons");
                if (lessonsResponse.IsSuccessStatusCode)
                {
                    var lessonsContent = await lessonsResponse.Content.ReadAsStringAsync();
                    var allLessons = JsonSerializer.Deserialize<IEnumerable<LessonViewModel>>(lessonsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    Lessons = allLessons.Where(l => l.CourseId == courseId).OrderBy(l => l.OrderIndex);
                }
                else
                {
                    Lessons = new List<LessonViewModel>();
                }
            }

            return Page();
        }
    }
}