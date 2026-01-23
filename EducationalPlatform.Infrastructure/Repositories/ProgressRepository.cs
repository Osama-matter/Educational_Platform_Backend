using Ardalis.Specification.EntityFrameworkCore;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities.progress;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class ProgressRepository : ILessonProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public ProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(LessonProgress LessonProgress)
        {
            await _context.LessonProgresses.AddAsync(LessonProgress);
            await _context.SaveChangesAsync();
        }

        public  void  DeleteAsync(LessonProgress LessonProgress)
        {
          
            _context.LessonProgresses.Remove(LessonProgress);
            _context.SaveChanges();
          
        }

        public async Task<IEnumerable<LessonProgress>> GetAllAsync()
        {
            var lessonProgresses = await _context.LessonProgresses.ToListAsync();
            return lessonProgresses;

        }

        public async Task<LessonProgress> GetByIdAsync(Guid id)
        {
            var lessonProgress =await _context.LessonProgresses.FirstOrDefaultAsync(lp => lp.Id == id);
            return lessonProgress;
        }

    }
}
