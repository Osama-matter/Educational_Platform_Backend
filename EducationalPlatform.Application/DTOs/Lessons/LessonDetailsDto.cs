using System;
using System.Collections.Generic;
using EducationalPlatform.Application.DTOs.Quiz;
using EducationalPlatform.Application.DTOs.QuizAttempt;

namespace EducationalPlatform.Application.DTOs.Lessons
{
    public class LessonDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? DurationMinutes { get; set; }
        public int OrderIndex { get; set; }
        public QuizSummaryDto Quiz { get; set; }
        public List<QuizAttemptDto> QuizAttempts { get; set; } = new();
    }
}
