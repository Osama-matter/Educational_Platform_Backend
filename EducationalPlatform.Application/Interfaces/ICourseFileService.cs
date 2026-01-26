using EducationalPlatform.Application.DTOs.CourseFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces
{
    public  interface ICourseFileService
    {
        public Task<CourseFileDto> CreateAsync(CreateCourseFileRequest request);
        public Task<CourseFileDto> GetByIdAsync(Guid id);
        public Task<IEnumerable<CourseFileDto>> GetAllAsync();
        public Task<CourseFileDto> UpdateAsync(Guid id, UpdateCourseFileRequest request);
        public Task DeleteAsync(Guid id);

    }
}
