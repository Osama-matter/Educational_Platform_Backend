using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Lesson
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(10000)]
        public string Content { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        public int? DurationMinutes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Course Course { get; set; }
        public virtual ICollection<LessonProgress> LessonProgresses { get; set; }

        public Lesson()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            LessonProgresses = new HashSet<LessonProgress>();
        }
    }
}
