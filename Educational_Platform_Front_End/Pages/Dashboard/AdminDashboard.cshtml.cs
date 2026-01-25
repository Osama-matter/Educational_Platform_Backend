using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                var coursesResponse = await client.GetAsync("https://localhost:7228/api/courses");
                if (coursesResponse.IsSuccessStatusCode)
                {
                    var coursesContent = await coursesResponse.Content.ReadAsStringAsync();
                    Courses = JsonSerializer.Deserialize<List<CourseViewModel>>(coursesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    Courses = new List<CourseViewModel>();
                }
            }
            return Page();
        }
    }
}
