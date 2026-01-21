using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id);
    }
}
