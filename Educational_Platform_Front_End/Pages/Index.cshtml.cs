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
        private readonly IConfiguration _configuration;

        public IEnumerable<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            var baseUrl = _configuration["ApiConfig:BaseUrl"] ?? "https://matterhub.runasp.net";
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{baseUrl}/api/courses");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(content))
                        {
                            try {
                                var allCourses = JsonSerializer.Deserialize<IEnumerable<CourseViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                                Courses = allCourses?.ToList() ?? new List<CourseViewModel>();
                                _logger.LogInformation("Successfully loaded {Count} courses on Home page.", Courses.Count());
                            } catch (JsonException jex) {
                                _logger.LogError(jex, "JSON Deserialization error on Home page. Raw content: {Content}", content);
                                Courses = new List<CourseViewModel>();
                            }
                        }
                        else
                        {
                            Courses = new List<CourseViewModel>();
                            _logger.LogWarning("API returned empty content for courses on Home page.");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        _logger.LogError("API Error ({StatusCode}) on Home page: {Error}", response.StatusCode, errorContent);
                        Courses = new List<CourseViewModel>();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "System error fetching courses for Home page.");
                    Courses = new List<CourseViewModel>();
                }
            }
        }
    }
}
