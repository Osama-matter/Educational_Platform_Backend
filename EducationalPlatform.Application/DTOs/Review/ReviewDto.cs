using System;

namespace EducationalPlatform.Application.DTOs.Review
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } 
        public Guid CourseId { get; set; }
        public string? InstructorReply { get; set; }
    }
}
