using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces
{
    public interface ICourseService 
    {
        public Task<CourseDto> CreateAsync(CreateCourseDto request);
        public Task<CourseDto> GetByIdAsync(Guid id);
        public Task<IEnumerable<CourseDto>> GetAllAsync();
        public Task<CourseDto> UpdateAsync(Guid id, UpdateCourseDto request);
        public Task<bool> DeleteAsync(Guid id);
    }
}
