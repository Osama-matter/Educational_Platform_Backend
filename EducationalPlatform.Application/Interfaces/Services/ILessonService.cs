using EducationalPlatform.Application.DTOs.Lessons;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface ILessonService
    {
        public Task<LessonDto> CreateAsync(CreateLessonDto request);
        public Task<LessonDto> GetByIdAsync(Guid id);
   
        public Task<IEnumerable<LessonDto>> GetAllLessonsForCourseAsync(Guid courseId);
        public Task<IEnumerable<LessonDto>> GetAllAsync();
        public Task<LessonDto> UpdateAsync(Guid id, UpdateLessonDto request);
        public Task<bool> DeleteAsync(Guid id);
    }
}
