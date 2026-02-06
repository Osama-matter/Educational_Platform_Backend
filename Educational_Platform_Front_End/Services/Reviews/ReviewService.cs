using EducationalPlatform.Application.DTOs.Review;
using System.Net.Http.Json;

namespace Educational_Platform_Front_End.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReviewDto> GetReviewByIdAsync(Guid reviewId)
        {
            return await _httpClient.GetFromJsonAsync<ReviewDto>($"api/Reviews/{reviewId}");
        }

        public async Task<List<ReviewDto>> GetReviewsForCourseAsync(Guid courseId)
        {
            return await _httpClient.GetFromJsonAsync<List<ReviewDto>>($"api/Reviews/course/{courseId}");
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto review)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Reviews", review);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReviewDto>();
        }

        public async Task UpdateReviewAsync(Guid reviewId, UpdateReviewDto review)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Reviews/{reviewId}", review);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteReviewAsync(Guid reviewId)
        {
            var response = await _httpClient.DeleteAsync($"api/Reviews/{reviewId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task ReplyToReviewAsync(Guid reviewId, InstructorReplyDto reply)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Reviews/{reviewId}/reply", reply);
            response.EnsureSuccessStatusCode();
        }
    }
}
