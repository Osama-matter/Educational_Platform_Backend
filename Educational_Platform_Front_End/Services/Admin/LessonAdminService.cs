using Educational_Platform_Front_End.Models.Lessons;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public class LessonAdminService : ILessonAdminService
    {
        private const string LessonsApiBase = "https://localhost:7228/api/lessons";

        public async Task<IReadOnlyList<LessonViewModel>> GetLessonsAsync(string token)
        {
            using var client = CreateClient(token);
            var response = await client.GetAsync(LessonsApiBase);
            if (!response.IsSuccessStatusCode)
            {
                return Array.Empty<LessonViewModel>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var lessons = JsonSerializer.Deserialize<List<LessonViewModel>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return lessons ?? new List<LessonViewModel>();
        }

        public async Task<LessonViewModel> GetLessonByIdAsync(string token, Guid lessonId)
        {
            using var client = CreateClient(token);
            var response = await client.GetAsync($"{LessonsApiBase}/{lessonId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LessonViewModel>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task CreateLessonAsync(string token, CreateLessonViewModel lesson)
        {
            using var client = CreateClient(token);
            var json = JsonSerializer.Serialize(lesson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(LessonsApiBase, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateLessonAsync(string token, Guid lessonId, UpdateLessonViewModel lesson)
        {
            using var client = CreateClient(token);
            var json = JsonSerializer.Serialize(lesson);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{LessonsApiBase}/{lessonId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteLessonAsync(string token, Guid lessonId)
        {
            using var client = CreateClient(token);
            var response = await client.DeleteAsync($"{LessonsApiBase}/{lessonId}");
            response.EnsureSuccessStatusCode();
        }

        private static HttpClient CreateClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }
    }
}
