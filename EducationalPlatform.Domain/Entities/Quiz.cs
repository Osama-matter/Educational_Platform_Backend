using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EducationalPlatform.Domain.Entities.Leeson;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }

        public int DurationMinutes { get; set; }
        public int TotalScore { get; set; }
        public int PassingScore { get; set; }

        public bool IsPublished { get; set; }

        // FK
        [ForeignKey("Lesson")]
        public Guid LessonId { get; set; }


        // Navigation
        public virtual Lesson Lesson { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }
}
