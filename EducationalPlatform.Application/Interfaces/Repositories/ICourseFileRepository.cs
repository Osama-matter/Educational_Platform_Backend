using EducationalPlatform.Domain.Entities.Course_File;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public  interface ICourseFileRepository
    {
        Task<CourseFile> GetByIdAsync(Guid id);
        Task<IEnumerable<CourseFile>> GetAllAsync();
        Task<IEnumerable<CourseFile>> GetByCourseIdAsync(Guid courseId);
        Task AddAsync(CourseFile CourseFile);
        Task UpdateAsync(CourseFile CourseFile);
        Task DeleteAsync(CourseFile courseFile);

    }
}
