using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;

        public QuestionType QuestionType { get; set; }

        public int Score { get; set; }

        // FK

        [ForeignKey("Quiz")]
        public Guid QuizId { get; set; }

        // Navigation
        public Quiz Quiz { get; set; } = null!;
        public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    }
}
