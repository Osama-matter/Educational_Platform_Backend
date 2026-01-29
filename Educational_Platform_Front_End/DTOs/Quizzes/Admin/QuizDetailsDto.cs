using Educational_Platform_Front_End.DTOs.Questions;

namespace Educational_Platform_Front_End.DTOs.Quizzes.Admin
{
    public class QuizDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public bool IsPublished { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }
}
