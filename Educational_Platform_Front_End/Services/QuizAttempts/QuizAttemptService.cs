using EducationalPlatform.Application.DTOs.QuizAttempt;
using EducationalPlatform.Application.DTOs.Answer;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.QuizAttempts
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly HttpClient _httpClient;

        public QuizAttemptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateQuizAttemptAsync(CreateQuizAttemptDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/QuizAttempts", createDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<QuizAttemptDto> GetQuizAttemptByIdAsync(Guid attemptId)
        {
            return await _httpClient.GetFromJsonAsync<QuizAttemptDto>($"api/QuizAttempts/{attemptId}");
        }

        public async Task SubmitAnswersAsync(Guid attemptId, SubmitAnswersRequest answers)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/QuizAttempts/{attemptId}/submit", answers);
            response.EnsureSuccessStatusCode();
        }
    }
}
