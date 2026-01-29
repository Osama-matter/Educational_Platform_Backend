using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationalPlatform.Domain.Entities
{
    public class QuestionOption
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public bool IsCorrect { get; set; }

        // FK
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }

        // Navigation
        public virtual Question Question { get; set; } = null!;
    }
}
