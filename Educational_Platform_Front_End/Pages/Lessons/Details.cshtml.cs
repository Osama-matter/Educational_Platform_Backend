using Educational_Platform_Front_End.Models.Lessons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        public LessonViewModel Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7228/api/lessons/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Lesson = JsonSerializer.Deserialize<LessonViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
