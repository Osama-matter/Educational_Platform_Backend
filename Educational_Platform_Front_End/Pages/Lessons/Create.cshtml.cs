using Educational_Platform_Front_End.Models.Lessons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Lessons
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateLessonViewModel Lesson { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid CourseId { get; set; }

        public void OnGet()
        {
            Lesson = new CreateLessonViewModel { CourseId = CourseId };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json = JsonSerializer.Serialize(Lesson);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7228/api/lessons", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Lessons/Index", new { courseId = Lesson.CourseId });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"An error occurred: {errorContent}");
                    return Page();
                }
            }
        }
    }
}
