using Ardalis.Specification.EntityFrameworkCore;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities.Course_File;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class CourseFileRepository : ICourseFileRepository
    {
        ApplicationDbContext _context;
        public CourseFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CourseFile CourseFile)
        {
             await _context.CourseFiles.AddAsync(CourseFile);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(CourseFile courseFile)
        {
            _context.CourseFiles.Remove(courseFile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseFile>> GetAllAsync()
        {
            return await _context.CourseFiles.ToListAsync();
            
        }

        public async Task<CourseFile> GetByIdAsync(Guid id)
        {
            return await _context.CourseFiles.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(CourseFile CourseFile)
        {
            _context.CourseFiles.Update(CourseFile);
            await _context.SaveChangesAsync();
        }
    }
}
