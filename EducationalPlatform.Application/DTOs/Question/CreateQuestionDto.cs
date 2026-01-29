using EducationalPlatform.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Application.DTOs.Question
{
    public class CreateQuestionDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public Guid QuizId { get; set; }
    }
}
