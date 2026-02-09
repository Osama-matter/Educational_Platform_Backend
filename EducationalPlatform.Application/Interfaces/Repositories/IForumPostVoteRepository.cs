using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IForumPostVoteRepository
    {
        Task<ForumPostVote?> GetByPostAndUserAsync(Guid postId, Guid userId);
        Task AddAsync(ForumPostVote vote);
        Task UpdateAsync(ForumPostVote vote);
        Task DeleteAsync(ForumPostVote vote);
        Task<ForumPostVote?> GetByThreadAndUserAsync(Guid threadId, Guid userId);
        Task<int> GetVoteCountAsync(Guid postId);
        Task<int> GetThreadVoteCountAsync(Guid threadId);
    }
}
