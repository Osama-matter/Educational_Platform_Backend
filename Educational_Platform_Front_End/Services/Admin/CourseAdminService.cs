using Educational_Platform_Front_End.Models.Courses;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public class CourseAdminService : ICourseAdminService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseAdminService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyList<CourseViewModel>> GetCoursesAsync(string token)
        {
            // This method might be obsolete now but is kept to satisfy the interface.
            // The new approach uses a centrally configured HttpClient.
            return await _httpClient.GetFromJsonAsync<IReadOnlyList<CourseViewModel>>("api/courses");
        }

        public async Task<IEnumerable> GetAllCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CourseViewModel>>("api/courses");
        }
    }
}
