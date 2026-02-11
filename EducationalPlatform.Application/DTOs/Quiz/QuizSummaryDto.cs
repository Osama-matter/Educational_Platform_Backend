using System;

namespace EducationalPlatform.Application.DTOs.Quiz
{
    public class QuizSummaryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int DurationMinutes { get; set; }
        public Guid LessonId { get; set; }
    }
}
