using System;
using EducationalPlatform.Domain.Entities.progress;

namespace EducationalPlatform.Application.DTOs.Progress
{
    public class LessonProgressDto
    {
        public Guid Id { get; set; }
        public Guid EnrollmentId { get; set; }
        public Guid LessonId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public LessonProgressDto()
        {
        }

        public LessonProgressDto(LessonProgress progress)
        {
            Id = progress.Id;
            EnrollmentId = progress.EnrollmentId;
            LessonId = progress.LessonId;
            IsCompleted = progress.IsCompleted;
            CompletedAt = progress.CompletedAt;
        }
    }
}
