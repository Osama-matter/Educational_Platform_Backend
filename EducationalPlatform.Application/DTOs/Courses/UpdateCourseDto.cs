using System;
using EducationalPlatform.Domain.Entities.Course;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class UpdateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }

        public void ApplyTo(Course course)
        {
            course.Title = Title;
            course.Description = Description;
            course.EstimatedDurationHours = EstimatedDurationHours;
            course.IsActive = IsActive;
        }
    }
}
