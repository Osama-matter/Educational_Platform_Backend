using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities.Leeson;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {

        private readonly ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Lesson Lesson)
        {
            _context.Lessons.Add(Lesson);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Lesson lesson)
        {
      
            
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
          

        }

        public async Task<IEnumerable<Lesson>> GetAllAsync()
        {
            return  await  _context.Lessons.ToListAsync();
            
        }

        public async Task<Lesson> GetByIdAsync(Guid id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(e => e.Id == id);
            return lesson;
        }

        public async Task UpdateAsync(Lesson Lesson)
        {
            _context.Lessons.Update(Lesson);
            await _context.SaveChangesAsync();
        }
    }
}
