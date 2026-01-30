using EducationalPlatform.Application.DTOs.QuestionOption;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.Questions
{
    public class QuestionOptionService : IQuestionOptionService
    {
        private readonly HttpClient _httpClient;

        public QuestionOptionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateQuestionOptionAsync(CreateQuestionOptionDto option)
        {
            await _httpClient.PostAsJsonAsync("api/QuestionOptions", option);
        }

        public async Task DeleteQuestionOptionAsync(Guid optionId)
        {
            await _httpClient.DeleteAsync($"api/QuestionOptions/{optionId}");
        }
    }
}
