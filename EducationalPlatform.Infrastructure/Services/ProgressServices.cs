using EducationalPlatform.Application.DTOs.Progress;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities.progress;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NuGet.Protocol.VisualStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class ProgressServices : IProgressService
    {
        private readonly ILessonProgressRepository _lessonProgressRepository;
        public ProgressServices(ILessonProgressRepository lessonProgressRepository)
        {
            _lessonProgressRepository = lessonProgressRepository;
        }
        public async Task<LessonProgressDto> CreateAsync(CreateLessonProgressDto createLessonProgress)
        {
            var  lessonProgressEntity = createLessonProgress.ToEntity();
            await _lessonProgressRepository.AddAsync(lessonProgressEntity);
            var dto = new LessonProgressDto
            {
                Id = lessonProgressEntity.Id,
                EnrollmentId = lessonProgressEntity.EnrollmentId,
                LessonId = lessonProgressEntity.LessonId,
                IsCompleted = lessonProgressEntity.IsCompleted,
                CompletedAt = lessonProgressEntity.CompletedAt
            };
            return dto ; 

        }

        public  void   DeleteAsync(Guid id)
        {
            var lessonProgress = _lessonProgressRepository.GetByIdAsync(id).Result;
            if (lessonProgress == null)
            {
                throw new Exception("Lesson progress not found");
            }
            _lessonProgressRepository.DeleteAsync(lessonProgress);
           
        }

        public async Task<IEnumerable<LessonProgressDto>> GetAllAsync()
        {
            var data = await _lessonProgressRepository.GetAllAsync();
            if (data == null || !data.Any())
            {
                throw new Exception("No lesson progress found");
            }
            var Oresult = data.Select(lp => new LessonProgressDto
            {
                Id = lp.Id,
                EnrollmentId = lp.EnrollmentId,
                LessonId = lp.LessonId,
                IsCompleted = lp.IsCompleted,
                CompletedAt = lp.CompletedAt
            });
            return Oresult;
        }

        public async Task<LessonProgressDto> GetByIdAsync(Guid id)
        {
            var lessonProgress = _lessonProgressRepository.GetByIdAsync(id).Result;
            if (lessonProgress == null)
            {
                throw new Exception("Lesson progress not found");
            }
            var OResult =  new LessonProgressDto
            {
                Id = lessonProgress.Id,
                EnrollmentId = lessonProgress.EnrollmentId,
                LessonId = lessonProgress.LessonId,
                IsCompleted = lessonProgress.IsCompleted,
                CompletedAt = lessonProgress.CompletedAt
            };
            return OResult;
        }
    }
}
