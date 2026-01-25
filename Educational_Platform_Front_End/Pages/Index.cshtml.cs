using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://localhost:7228/api/courses");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        Courses = JsonSerializer.Deserialize<IEnumerable<CourseViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "Error fetching courses from API.");
                    Courses = new List<CourseViewModel>();
                }
            }
        }
    }
}
