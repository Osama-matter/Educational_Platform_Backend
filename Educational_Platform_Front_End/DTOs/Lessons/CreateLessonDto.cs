using System;


namespace EducationalPlatform.Application.DTOs.Lessons
{
    public class CreateLessonDto
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
        public int? DurationMinutes { get; set; }


    }
}
