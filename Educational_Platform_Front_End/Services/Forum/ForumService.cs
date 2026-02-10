using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EducationalPlatform.Application.DTOs.Forum;

namespace Educational_Platform_Front_End.Services.Forum
{
    public class ForumService : IForumService
    {
        private readonly HttpClient _httpClient;

        public ForumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ForumThreadDto>> GetAllThreadsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ForumThreads");
                
                if (!response.IsSuccessStatusCode)
                {
                    return new List<ForumThreadDto>();
                }
                
                return await response.Content.ReadFromJsonAsync<IEnumerable<ForumThreadDto>>() ?? new List<ForumThreadDto>();
            }
            catch
            {
                return new List<ForumThreadDto>();
            }
        }

        public async Task<ForumThreadDto> GetThreadByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ForumThreadDto>($"api/ForumThreads/{id}");
        }

        public async Task<ForumThreadDto> CreateThreadAsync(CreateForumThreadDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ForumThreads", createDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ForumThreadDto>();
        }

        public async Task<ForumThreadDto> UpdateThreadAsync(Guid id, UpdateForumThreadDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ForumThreads/{id}", updateDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ForumThreadDto>();
        }

        public async Task<bool> DeleteThreadAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/ForumThreads/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ForumPostDto>> GetThreadPostsAsync(Guid threadId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ForumPostDto>>($"api/ForumThreads/{threadId}/posts");
        }

        // Posts
        public async Task<ForumPostDto> GetPostByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ForumPostDto>($"api/ForumPosts/{id}");
        }

        public async Task<ForumPostDto> CreatePostAsync(CreateForumPostDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ForumPosts", createDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ForumPostDto>();
        }

        public async Task<ForumPostDto> UpdatePostAsync(Guid id, UpdateForumPostDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ForumPosts/{id}", updateDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ForumPostDto>();
        }

        public async Task<bool> DeletePostAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/ForumPosts/{id}");
            return response.IsSuccessStatusCode;
        }

        // Subscriptions
        public async Task<IEnumerable<ForumSubscriptionDto>> GetMySubscriptionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ForumSubscriptionDto>>("api/ForumSubscriptions/my-subscriptions");
        }

        public async Task<ForumSubscriptionDto> SubscribeAsync(CreateForumSubscriptionDto subscribeDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ForumSubscriptions", subscribeDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ForumSubscriptionDto>();
        }

        public async Task<bool> UnsubscribeAsync(Guid threadId)
        {
            var response = await _httpClient.DeleteAsync($"api/ForumSubscriptions?threadId={threadId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsSubscribedAsync(Guid threadId)
        {
            return await _httpClient.GetFromJsonAsync<bool>($"api/ForumSubscriptions/is-subscribed/{threadId}");
        }

        // Voting
        public async Task<int> VoteAsync(Guid postId, int value)
        {
            var response = await _httpClient.PostAsync($"api/ForumVoting/{postId}/vote?value={value}", null);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<VoteResponse>();
            return result?.VoteCount ?? 0;
        }

        public async Task<int?> GetMyVoteAsync(Guid postId)
        {
            var response = await _httpClient.GetAsync($"api/ForumVoting/{postId}/my-vote");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadFromJsonAsync<MyVoteResponse>();
            return result?.Value;
        }

        public async Task<int> VoteThreadAsync(Guid threadId, int value)
        {
            var response = await _httpClient.PostAsync($"api/ForumVoting/thread/{threadId}/vote?value={value}", null);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<VoteResponse>();
            return result?.VoteCount ?? 0;
        }

        public async Task<int?> GetMyThreadVoteAsync(Guid threadId)
        {
            var response = await _httpClient.GetAsync($"api/ForumVoting/thread/{threadId}/my-vote");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadFromJsonAsync<MyVoteResponse>();
            return result?.Value;
        }

        private class VoteResponse
        {
            public int VoteCount { get; set; }
        }

        private class MyVoteResponse
        {
            public int? Value { get; set; }
        }
    }
}
