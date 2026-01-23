using Educational_Platform_Front_End.Models.Lessons;
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
    public class IndexModel : PageModel
    {
        public IEnumerable<LessonViewModel> Lessons { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7228/api/lessons");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var allLessons = JsonSerializer.Deserialize<IEnumerable<LessonViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    Lessons = allLessons.Where(l => l.CourseId == CourseId).OrderBy(l => l.OrderIndex);
                    return Page();
                }
                else
                {
                    Lessons = new List<LessonViewModel>();
                    return Page();
                }
            }
        }
    }
}
