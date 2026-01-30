using System;
using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class CourseDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LessonDetailsDto> Lessons { get; set; } = new();
    }
}
