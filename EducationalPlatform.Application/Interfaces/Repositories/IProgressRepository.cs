
using EducationalPlatform.Domain.Entities.progress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public   interface ILessonProgressRepository
    {
        Task<LessonProgress> GetByIdAsync(Guid id);
        Task<IEnumerable<LessonProgress>> GetAllAsync();
        Task AddAsync(LessonProgress LessonProgress);

        void  DeleteAsync(LessonProgress LessonProgress);

    }
}
