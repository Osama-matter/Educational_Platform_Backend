using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Application.DTOs.QuizAttempt
{
    public class CreateQuizAttemptDto
    {
        public Guid? UserId { get; set; }

        [Required]
        public Guid QuizId { get; set; }
    }
}
