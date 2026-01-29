using Educational_Platform_Front_End.DTOs.Questions;
using System.Collections.Generic;

namespace Educational_Platform_Front_End.DTOs.QuizAttempts
{
    public class QuizAttemptDto
    {
        public Guid Id { get; set; }
        public int TotalScore { get; set; }
        public string Status { get; set; }
        public string QuizTitle { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
