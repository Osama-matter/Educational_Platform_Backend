using EducationalPlatform.Application.DTOs.Review;
using System;
using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class CourseDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image_URl { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }
        public List<LessonDetailsDto> Lessons { get; set; } = new();
        public List<QuizSummaryDto> Quizzes { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
