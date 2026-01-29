using System;
using System.ComponentModel.DataAnnotations;

namespace EducationalPlatform.Application.DTOs.Quiz
{
    public class CreateQuizDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime AvailableFrom { get; set; }
        [Required]
        public DateTime AvailableTo { get; set; }
        [Required]
        public int DurationMinutes { get; set; }
        [Required]
        public int TotalScore { get; set; }
        [Required]
        public int PassingScore { get; set; }
        public bool IsPublished { get; set; }
        [Required]
        public Guid LessonId { get; set; }
    }
}
