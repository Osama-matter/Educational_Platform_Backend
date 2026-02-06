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
    public class ForumPostRepository : IForumPostRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ForumPost?> GetByIdAsync(Guid id)
        {
            return await _context.forumPosts
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ForumPost>> GetByThreadIdAsync(Guid threadId)
        {
            return await _context.forumPosts
                .Where(p => p.ForumThreadId == threadId)
                .Include(p => p.User)
                .OrderBy(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(ForumPost post)
        {
            await _context.forumPosts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForumPost post)
        {
            _context.forumPosts.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _context.forumPosts.FindAsync(id);
            if (post != null)
            {
                _context.forumPosts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
    }
}
