using System;
using System.Collections.Generic;
using EducationalPlatform.Application.DTOs.QuizAttempt;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class LessonDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public QuizSummaryDto Quiz { get; set; }
        public List<QuizAttemptDto> QuizAttempts { get; set; } = new();
    }
}
