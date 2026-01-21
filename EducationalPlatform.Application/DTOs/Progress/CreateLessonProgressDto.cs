using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Progress
{
    public class CreateLessonProgressDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid LessonId { get; set; }

        public LessonProgress ToEntity()
        {
            return new LessonProgress(EnrollmentId, LessonId);
        }
    }
}
