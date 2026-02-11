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
        private readonly IConfiguration _configuration;
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public bool IsAdmin { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            IsAdmin = User.IsInRole("Admin");
            var baseUrl = _configuration["ApiConfig:BaseUrl"] ?? "https://matterhub.runasp.net";

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"{baseUrl}/api/courses");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        
                        // Debug logging for the content received
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            ModelState.AddModelError(string.Empty, "API returned an empty response.");
                            Courses = new List<CourseViewModel>();
                            return Page();
                        }

                        var allCourses = JsonSerializer.Deserialize<IEnumerable<CourseViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        if (allCourses != null && allCourses.Any())
                        {
                            // Show all courses for debugging purposes
                            Courses = allCourses.ToList();
                        }
                        else
                        {
                            Courses = new List<CourseViewModel>();
                            ModelState.AddModelError(string.Empty, "API returned a successful status but no course data.");
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Courses = new List<CourseViewModel>();
                        ModelState.AddModelError(string.Empty, $"API Error ({response.StatusCode}): {errorContent}");
                    }
                }
                catch (JsonException ex)
                {
                    Courses = new List<CourseViewModel>();
                    ModelState.AddModelError(string.Empty, $"Data Format Error: The API returned data in an unexpected format. Details: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Courses = new List<CourseViewModel>();
                    ModelState.AddModelError(string.Empty, $"System Error: {ex.Message}");
                }
            }
            return Page();
        }
    }
}
