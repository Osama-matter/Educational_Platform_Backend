using System;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class QuizSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int DurationMinutes { get; set; }
        public Guid LessonId { get; set; }
    }
}
