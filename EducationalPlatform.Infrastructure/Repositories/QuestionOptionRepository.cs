using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class QuestionOptionRepository : IQuestionOptionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionOptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<QuestionOption> GetByIdAsync(Guid id)
        {
            return await _context.QuestionOptions.FindAsync(id);
        }

        public async Task<IEnumerable<QuestionOption>> GetAllAsync()
        {
            return await _context.QuestionOptions.ToListAsync();
        }

        public async Task AddAsync(QuestionOption questionOption)
        {
            await _context.QuestionOptions.AddAsync(questionOption);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuestionOption questionOption)
        {
            _context.QuestionOptions.Update(questionOption);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(QuestionOption questionOption)
        {
            _context.QuestionOptions.Remove(questionOption);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionOption>> GetByQuestionIdAsync(Guid questionId)
        {
            return await _context.QuestionOptions
                .AsNoTracking()
                .Where(qo => qo.QuestionId == questionId)
                .ToListAsync();
        }
    }
}
