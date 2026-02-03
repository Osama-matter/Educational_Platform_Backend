using System;

namespace EducationalPlatform.Application.DTOs.Review
{
    public class CreateReviewDto
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
