using System;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IForumVotingService
    {
        Task<int> VoteAsync(Guid postId, Guid userId, int value);
        Task<int> GetVoteCountAsync(Guid postId);
        Task<int?> GetUserVoteAsync(Guid postId, Guid userId);

        Task<int> VoteThreadAsync(Guid threadId, Guid userId, int value);
        Task<int> GetThreadVoteCountAsync(Guid threadId);
        Task<int?> GetUserThreadVoteAsync(Guid threadId, Guid userId);
    }
}
