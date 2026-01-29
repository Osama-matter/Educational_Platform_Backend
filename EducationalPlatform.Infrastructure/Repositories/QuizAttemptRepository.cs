using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class QuizAttemptRepository : IQuizAttemptRepository
    {
        private readonly ApplicationDbContext _context;

        public QuizAttemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuizAttempt> GetByIdAsync(Guid id)
        {
            return await _context.QuizAttempts.FirstOrDefaultAsync(e=>e.Id == id);
        }

        public async Task<IEnumerable<QuizAttempt>> GetAllAsync()
        {
            return await _context.QuizAttempts.ToListAsync();
        }

        public async Task AddAsync(QuizAttempt quizAttempt)
        {
            await _context.QuizAttempts.AddAsync(quizAttempt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuizAttempt quizAttempt)
        {
            _context.QuizAttempts.Update(quizAttempt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuizAttempt quizAttempt)
        {

            _context.QuizAttempts.Remove(quizAttempt);
            await _context.SaveChangesAsync();
          
        }

        public async Task<IEnumerable<QuizAttempt>> GetByUserIdAndQuizIdAsync(Guid userId, Guid quizId)
        {
            return await _context.QuizAttempts
                .Where(qa => qa.UserId == userId && qa.QuizId == quizId)
                .ToListAsync();
        }
    }
}

