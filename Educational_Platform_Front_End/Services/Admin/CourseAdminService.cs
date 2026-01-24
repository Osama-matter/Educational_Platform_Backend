using Educational_Platform_Front_End.Models.Courses;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public class CourseAdminService : ICourseAdminService
    {
        private const string CoursesApiBase = "https://localhost:7228/api/courses";

        public async Task<IReadOnlyList<CourseViewModel>> GetCoursesAsync(string token)
        {
            using var client = CreateClient(token);
            var response = await client.GetAsync(CoursesApiBase);
            if (!response.IsSuccessStatusCode)
            {
                return new List<CourseViewModel>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var courses = JsonSerializer.Deserialize<List<CourseViewModel>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return courses ?? new List<CourseViewModel>();
        }

        private static HttpClient CreateClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
