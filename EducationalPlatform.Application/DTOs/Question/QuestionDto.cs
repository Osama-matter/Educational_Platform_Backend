using EducationalPlatform.Domain.Enums;
using System;

namespace EducationalPlatform.Application.DTOs.Question
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public QuestionType QuestionType { get; set; }
        public int Score { get; set; }
        public Guid QuizId { get; set; }
    }
}
