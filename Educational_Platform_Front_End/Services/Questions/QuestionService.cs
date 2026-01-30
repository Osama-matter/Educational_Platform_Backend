using EducationalPlatform.Application.DTOs.Question;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly HttpClient _httpClient;

        public QuestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateQuestionAsync(CreateQuestionDto question)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Questions", question);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task DeleteQuestionAsync(Guid questionId)
        {
            await _httpClient.DeleteAsync($"api/Questions/{questionId}");
        }
    }
}
