using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class QuizAttempt
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("Quiz")]
        public Guid QuizId { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }

        public int TotalScore { get; set; }

        public QuizAttemptStatus Status { get; set; }

        // Navigation
        public Quiz Quiz { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
