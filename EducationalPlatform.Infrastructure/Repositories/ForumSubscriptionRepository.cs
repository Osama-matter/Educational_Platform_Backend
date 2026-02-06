using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class ForumSubscriptionRepository : IForumSubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumSubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ForumSubscriptions?> GetByIdAsync(Guid id)
        {
            return await _context.ForumSubscription
                .Include(s => s.Forumthread)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ForumSubscriptions>> GetByUserIdAsync(Guid userId)
        {
            return await _context.ForumSubscription
                .Where(s => s.UserId == userId)
                .Include(s => s.Forumthread)
                .ToListAsync();
        }

        public async Task<ForumSubscriptions?> GetByUserAndThreadAsync(Guid userId, Guid threadId)
        {
            return await _context.ForumSubscription
                .FirstOrDefaultAsync(s => s.UserId == userId && s.ForumThreadId == threadId);
        }

        public async Task AddAsync(ForumSubscriptions subscription)
        {
            await _context.ForumSubscription.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var subscription = await _context.ForumSubscription.FindAsync(id);
            if (subscription != null)
            {
                _context.ForumSubscription.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
