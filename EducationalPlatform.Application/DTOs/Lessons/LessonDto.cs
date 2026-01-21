using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Lessons
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
        public int? DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public LessonDto()
        {
        }

        public LessonDto(Lesson lesson)
        {
            Id = lesson.Id;
            CourseId = lesson.CourseId;
            Title = lesson.Title;
            Content = lesson.Content;
            OrderIndex = lesson.OrderIndex;
            DurationMinutes = lesson.DurationMinutes;
            CreatedAt = lesson.CreatedAt;
            UpdatedAt = lesson.UpdatedAt;
        }
    }
}
