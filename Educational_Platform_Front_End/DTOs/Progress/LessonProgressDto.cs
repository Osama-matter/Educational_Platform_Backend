using System;


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


    }
}
