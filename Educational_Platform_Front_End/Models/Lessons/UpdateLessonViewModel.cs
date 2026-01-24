using System;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform_Front_End.Models.Lessons
{
    public class UpdateLessonViewModel
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int OrderIndex { get; set; }

        public int? DurationMinutes { get; set; }
    }
}
