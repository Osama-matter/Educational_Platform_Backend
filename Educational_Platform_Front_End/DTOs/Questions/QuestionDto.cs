using System.Collections.Generic;

namespace Educational_Platform_Front_End.DTOs.Questions
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public List<QuestionOptionDto> Options { get; set; } = new();
    }
}
