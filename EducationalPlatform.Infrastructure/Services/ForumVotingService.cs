using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class ForumVotingService : IForumVotingService
    {
        private readonly IForumPostVoteRepository _voteRepository;
        private readonly IForumPostRepository _postRepository;
        private readonly IForumThreadRepository _threadRepository;

        public ForumVotingService(
            IForumPostVoteRepository voteRepository, 
            IForumPostRepository postRepository,
            IForumThreadRepository threadRepository)
        {
            _voteRepository = voteRepository;
            _postRepository = postRepository;
            _threadRepository = threadRepository;
        }

        public async Task<int> VoteAsync(Guid postId, Guid userId, int value)
        {
            if (value != 1 && value != -1 && value != 0)
                throw new ArgumentException("Invalid vote value.");

            var existingVote = await _voteRepository.GetByPostAndUserAsync(postId, userId);
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) throw new Exception("Post not found");

            if (existingVote != null)
            {
                if (value == 0 || existingVote.Value == value)
                {
                    await _voteRepository.DeleteAsync(existingVote);
                }
                else
                {
                    existingVote.Value = value;
                    await _voteRepository.UpdateAsync(existingVote);
                }
            }
            else if (value != 0)
            {
                var newVote = new ForumPostVote
                {
                    Id = Guid.NewGuid(),
                    ForumPostId = postId,
                    UserId = userId,
                    Value = value,
                    CreatedAt = DateTime.UtcNow
                };
                await _voteRepository.AddAsync(newVote);
            }

            var newTotal = await _voteRepository.GetVoteCountAsync(postId);
            post.VoteCount = newTotal;
            await _postRepository.UpdateAsync(post);

            return newTotal;
        }

        public async Task<int> GetVoteCountAsync(Guid postId)
        {
            return await _voteRepository.GetVoteCountAsync(postId);
        }

        public async Task<int?> GetUserVoteAsync(Guid postId, Guid userId)
        {
            var vote = await _voteRepository.GetByPostAndUserAsync(postId, userId);
            return vote?.Value;
        }

        public async Task<int> VoteThreadAsync(Guid threadId, Guid userId, int value)
        {
            if (value != 1 && value != -1 && value != 0)
                throw new ArgumentException("Invalid vote value.");

            var existingVote = await _voteRepository.GetByThreadAndUserAsync(threadId, userId);
            var thread = await _threadRepository.GetByIdAsync(threadId);
            if (thread == null) throw new Exception("Thread not found");

            if (existingVote != null)
            {
                if (value == 0 || existingVote.Value == value)
                {
                    await _voteRepository.DeleteAsync(existingVote);
                }
                else
                {
                    existingVote.Value = value;
                    await _voteRepository.UpdateAsync(existingVote);
                }
            }
            else if (value != 0)
            {
                var newVote = new ForumPostVote
                {
                    Id = Guid.NewGuid(),
                    ForumThreadId = threadId,
                    UserId = userId,
                    Value = value,
                    CreatedAt = DateTime.UtcNow
                };
                await _voteRepository.AddAsync(newVote);
            }

            var newTotal = await _voteRepository.GetThreadVoteCountAsync(threadId);
            thread.VoteCount = newTotal;
            await _threadRepository.UpdateAsync(thread);

            return newTotal;
        }

        public async Task<int> GetThreadVoteCountAsync(Guid threadId)
        {
            return await _voteRepository.GetThreadVoteCountAsync(threadId);
        }

        public async Task<int?> GetUserThreadVoteAsync(Guid threadId, Guid userId)
        {
            var vote = await _voteRepository.GetByThreadAndUserAsync(threadId, userId);
            return vote?.Value;
        }
    }
}
