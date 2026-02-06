using Educational_Platform_Front_End.Models.Lessons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public class LessonAdminService : ILessonAdminService
    {
        private readonly HttpClient _httpClient;

        public LessonAdminService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<LessonViewModel>> GetLessonsAsync(string token)
        {
            return await _httpClient.GetFromJsonAsync<IReadOnlyList<LessonViewModel>>("api/Lessons");
        }

        public async Task<LessonViewModel> GetLessonByIdAsync(string token, Guid lessonId)
        {
            return await _httpClient.GetFromJsonAsync<LessonViewModel>($"api/Lessons/{lessonId}");
        }

        public async Task CreateLessonAsync(string token, CreateLessonViewModel lesson)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Lessons", lesson);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateLessonAsync(string token, Guid lessonId, UpdateLessonViewModel lesson)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Lessons/{lessonId}", lesson);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLessonAsync(string token, Guid lessonId)
        {
            var response = await _httpClient.DeleteAsync($"api/Lessons/{lessonId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable> GetLessonsForCourseAsync(Guid selectedCourseId)
        {
            return await _httpClient.GetFromJsonAsync<List<LessonViewModel>>($"api/Courses/{selectedCourseId}/lessons");
        }
    }
}
