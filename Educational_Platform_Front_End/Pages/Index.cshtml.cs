using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7228/api/courses");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Courses = JsonSerializer.Deserialize<IEnumerable<CourseViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    Courses = new List<CourseViewModel>();
                }

                return Page();
            }
        }
    }
}
