using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class IndexModel : PageModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public bool IsAdmin { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IsAdmin = User.IsInRole("Admin");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7228/api/courses");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var allCourses = JsonSerializer.Deserialize<IEnumerable<CourseViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (!IsAdmin)
                    {
                        Courses = allCourses.Where(c => c.IsActive).ToList();
                    }
                    else
                    {
                        Courses = allCourses.ToList();
                    }
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
