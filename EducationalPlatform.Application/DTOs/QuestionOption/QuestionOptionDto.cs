using System;

namespace EducationalPlatform.Application.DTOs.QuestionOption
{
    public class QuestionOptionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}
