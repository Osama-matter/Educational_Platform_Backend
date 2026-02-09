using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EducationalPlatform.Application.DTOs.Forum;

namespace Educational_Platform_Front_End.Services.Forum
{
    public interface IForumService
    {
        // Threads
        Task<IEnumerable<ForumThreadDto>> GetAllThreadsAsync();
        Task<ForumThreadDto> GetThreadByIdAsync(Guid id);
        Task<ForumThreadDto> CreateThreadAsync(CreateForumThreadDto createDto);
        Task<ForumThreadDto> UpdateThreadAsync(Guid id, UpdateForumThreadDto updateDto);
        Task<bool> DeleteThreadAsync(Guid id);
        Task<IEnumerable<ForumPostDto>> GetThreadPostsAsync(Guid threadId);

        // Posts
        Task<ForumPostDto> GetPostByIdAsync(Guid id);
        Task<ForumPostDto> CreatePostAsync(CreateForumPostDto createDto);
        Task<ForumPostDto> UpdatePostAsync(Guid id, UpdateForumPostDto updateDto);
        Task<bool> DeletePostAsync(Guid id);

        // Subscriptions
        Task<IEnumerable<ForumSubscriptionDto>> GetMySubscriptionsAsync();
        Task<ForumSubscriptionDto> SubscribeAsync(CreateForumSubscriptionDto subscribeDto);
        Task<bool> UnsubscribeAsync(Guid threadId);
        Task<bool> IsSubscribedAsync(Guid threadId);

        // Voting
        Task<int> VoteAsync(Guid postId, int value);
        Task<int?> GetMyVoteAsync(Guid postId);
        Task<int> VoteThreadAsync(Guid threadId, int value);
        Task<int?> GetMyThreadVoteAsync(Guid threadId);
    }
}
