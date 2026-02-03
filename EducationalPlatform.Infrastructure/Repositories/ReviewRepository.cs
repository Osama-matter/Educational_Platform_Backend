using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using EducationalPlatform.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review> GetByIdAsync(Guid id)
        {
            return await _context.Reviews
                .Include(e=>e.User)
                .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<IEnumerable<Review>> GetAllRewviewByCouseIDAsync(Guid CourseId)
        {
            return await _context.Reviews
                .Where(e=>e.CourseId == CourseId)
                .Include(r => r.User)
                .ToListAsync();

        }

        public async Task AddAsync(Review entity)
        {
            await _context.AddAsync(entity);
            _context.SaveChanges();
        }

        public void Update(Review entity)
        {
            _context.Reviews.Update(entity);
            _context.SaveChanges();

        }

        public void Delete(Review entity)
        {
            _context.Reviews.Remove(entity);
            _context.SaveChanges();
        }
    }
}
