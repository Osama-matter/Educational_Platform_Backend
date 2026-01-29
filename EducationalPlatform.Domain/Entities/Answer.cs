using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }

        // FKs
        [ForeignKey("QuizAttempt")]
        public Guid QuizAttemptId { get; set; }

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }

        [ForeignKey("QuestionOption")]
        public Guid SelectedOptionId { get; set; }

        // Navigation
        public virtual QuizAttempt QuizAttempt { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public virtual QuestionOption SelectedOption { get; set; } = null!;
    }
}
