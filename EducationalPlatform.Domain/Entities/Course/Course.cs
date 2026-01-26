using EducationalPlatform.Domain.Entities.Course_File;
using EducationalPlatform.Domain.Entities.Leeson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities.Course
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Instructor))]
        public Guid InstructorId { get; set; }

        public int? EstimatedDurationHours { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Image_URl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }




        // Navigation properties
        public virtual User Instructor { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<CourseFile> CourseFiles { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Lessons = new HashSet<Lesson>();
            Enrollments = new HashSet<Enrollment>();
        }

        public Course(string title, string description, Guid instructorId, int? estimatedDurationHours, bool isActive , string imageUrl)
            : this()
        {
            Title = title;
            Description = description;
            InstructorId = instructorId;
            EstimatedDurationHours = estimatedDurationHours;
            IsActive = isActive;
            Image_URl = imageUrl;
        }
    }
}
