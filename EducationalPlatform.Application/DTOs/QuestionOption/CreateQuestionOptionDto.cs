using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Application.DTOs.QuestionOption
{
    public class CreateQuestionOptionDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public Guid QuestionId { get; set; }
    }
}
