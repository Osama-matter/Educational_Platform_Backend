using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class ForumThreadRepository : IForumThreadRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumThreadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ForumThread?> GetByIdAsync(Guid id)
        {
            return await _context.ForumThreads
                .Include(t => t.User)
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<ForumThread>> GetAllAsync()
        {
            return await _context.ForumThreads
                .Include(t => t.User)
                .Include(t => t.Posts)
                .OrderByDescending(t => t.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(ForumThread thread)
        {
            await _context.ForumThreads.AddAsync(thread);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ForumThread thread)
        {
            _context.ForumThreads.Update(thread);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var thread = await _context.ForumThreads.FindAsync(id);
            if (thread != null)
            {
                _context.ForumThreads.Remove(thread);
                await _context.SaveChangesAsync();
            }
        }
    }
}
