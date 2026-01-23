using System;


namespace EducationalPlatform.Application.DTOs.Progress
{
    public class CreateLessonProgressDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid LessonId { get; set; }

    }
}
