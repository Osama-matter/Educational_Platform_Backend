using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Lessons
{
    public class UpdateLessonDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
        public int? DurationMinutes { get; set; }

        public void ApplyTo(Lesson lesson)
        {
            lesson.Title = Title;
            lesson.Content = Content;
            lesson.OrderIndex = OrderIndex;
            lesson.DurationMinutes = DurationMinutes;
        }
    }
}
