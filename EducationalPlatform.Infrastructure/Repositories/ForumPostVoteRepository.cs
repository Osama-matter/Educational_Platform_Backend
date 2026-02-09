using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class ForumPostVoteRepository : IForumPostVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumPostVoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ForumPostVote?> GetByPostAndUserAsync(Guid postId, Guid userId)
        {
            return await _context.ForumPostVotes
                .FirstOrDefaultAsync(v => v.ForumPostId == postId && v.UserId == userId);
        }

        public async Task AddAsync(ForumPostVote vote)
        {
            await _context.ForumPostVotes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForumPostVote vote)
        {
            _context.ForumPostVotes.Update(vote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ForumPostVote vote)
        {
            _context.ForumPostVotes.Remove(vote);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetVoteCountAsync(Guid postId)
        {
            return await _context.ForumPostVotes
                .Where(v => v.ForumPostId == postId)
                .SumAsync(v => v.Value);
        }

        public async Task<ForumPostVote?> GetByThreadAndUserAsync(Guid threadId, Guid userId)
        {
            return await _context.ForumPostVotes
                .FirstOrDefaultAsync(v => v.ForumThreadId == threadId && v.UserId == userId);
        }

        public async Task<int> GetThreadVoteCountAsync(Guid threadId)
        {
            return await _context.ForumPostVotes
                .Where(v => v.ForumThreadId == threadId)
                .SumAsync(v => v.Value);
        }
    }
}
