using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class LessonProgress
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Enrollment))]
        public Guid EnrollmentId { get; set; }

        [Required]
        [ForeignKey(nameof(Lesson))]
        public Guid LessonId { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public DateTime? CompletedAt { get; set; }

        // Navigation properties
        public virtual Enrollment Enrollment { get; set; }
        public virtual Lesson Lesson { get; set; }

        public LessonProgress()
        {
            Id = Guid.NewGuid();
            IsCompleted = false;
        }

        public LessonProgress(Guid enrollmentId, Guid lessonId)
            : this()
        {
            EnrollmentId = enrollmentId;
            LessonId = lessonId;
        }
    }
}
