using Educational_Platform_Front_End.DTOs.Courses;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.Courses
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _httpClient;

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CourseDetailsDto> GetCourseDetailsAsync(Guid courseId)
        {
            // This assumes your backend has an endpoint like /api/Courses/{courseId}
            // that returns the course with its lessons and quizzes.
            return await _httpClient.GetFromJsonAsync<CourseDetailsDto>($"api/Courses/{courseId}");
        }
    }
}
