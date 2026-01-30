using EducationalPlatform.Application.DTOs.Question;
using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.QuizAttempt
{
    public class QuizAttemptDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid QuizId { get; set; }
        public string QuizTitle { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int TotalScore { get; set; }
        public QuizAttemptStatus Status { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
