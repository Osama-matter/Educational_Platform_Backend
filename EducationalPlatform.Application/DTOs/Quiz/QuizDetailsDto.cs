using EducationalPlatform.Application.DTOs.Question;
using System.Collections.Generic;

namespace EducationalPlatform.Application.DTOs.Quiz
{
    public class QuizDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public bool IsPublished { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
