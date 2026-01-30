using EducationalPlatform.Application.DTOs.Quiz;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.Quizzes
{
    public class QuizService : IQuizService
    {
        private readonly HttpClient _httpClient;

        public QuizService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<QuizDto>> GetAllQuizzesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<QuizDto>>("api/Quizzes");
        }

        public async Task<QuizDto> GetQuizByIdAsync(Guid quizId)
        {
            return await _httpClient.GetFromJsonAsync<QuizDto>($"api/Quizzes/{quizId}");
        }

        public async Task<QuizDetailsDto> GetQuizDetailsForAdminAsync(Guid quizId)
        {
            // This assumes a new or existing endpoint that returns the detailed quiz structure.
            return await _httpClient.GetFromJsonAsync<QuizDetailsDto>($"api/Quizzes/admin/{quizId}");
        }

        public async Task<Guid> CreateQuizAsync(CreateQuizDto quiz)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Quizzes", quiz);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task UpdateQuizAsync(Guid quizId, QuizDto quiz)
        {
            await _httpClient.PutAsJsonAsync($"api/Quizzes/{quizId}", quiz);
        }

        public async Task DeleteQuizAsync(Guid quizId)
        {
            await _httpClient.DeleteAsync($"api/Quizzes/{quizId}");
        }

        public async Task PublishQuizAsync(Guid quizId)
        {
            // This assumes an endpoint that handles the publish action, likely a PUT or POST.
            var response = await _httpClient.PostAsync($"api/Quizzes/{quizId}/publish", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
