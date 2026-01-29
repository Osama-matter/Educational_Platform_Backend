using EducationalPlatform.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Application.DTOs.QuizAttempt
{
    public class UpdateQuizAttemptDto
    {
        [Required]
        public int TotalScore { get; set; }

        [Required]
        public QuizAttemptStatus Status { get; set; }
    }
}
