using EducationalPlatform.Application.DTOs.Progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces
{
    public interface  IProgressService
    {
        Task<LessonProgressDto> CreateAsync(CreateLessonProgressDto createLessonProgress);
        void  DeleteAsync(Guid id);
        Task<IEnumerable<LessonProgressDto>> GetAllAsync();
        Task<LessonProgressDto> GetByIdAsync(Guid id);
   


    }
}
