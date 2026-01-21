using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Enrollment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }

        [Required]
        public DateTime EnrolledAt { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation properties
        public virtual User Student { get; set; }
        public virtual Course.Course Course { get; set; }
        public virtual ICollection<LessonProgress> LessonProgresses { get; set; }

        public Enrollment()
        {
            Id = Guid.NewGuid();
            EnrolledAt = DateTime.UtcNow;
            IsActive = true;
            LessonProgresses = new HashSet<LessonProgress>();
        }

        public Enrollment(Guid studentId, Guid courseId)
            : this()
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
