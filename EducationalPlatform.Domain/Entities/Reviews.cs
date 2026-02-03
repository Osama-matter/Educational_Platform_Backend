using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string? Comment { get; set; }

        // Ensure this is restricted to 1-5 in your Application Layer
        [Range(1, 5)]
        public int Rate { get; set; }


        // Task Requirement: Instructor Responses
        public string? InstructorReply { get; set; }
        public DateTime? RepliedAt { get; set; }

        // Relationships
        public Guid CourseId { get; set; }
        public Course.Course Course { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Constructor
        public Review(string comment, int rate, Guid courseId, Guid userId)
        {
            Id = Guid.NewGuid();
            Comment = comment;
            Rate = rate;
            CourseId = courseId;
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
