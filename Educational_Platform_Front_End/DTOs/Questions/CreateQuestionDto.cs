namespace Educational_Platform_Front_End.DTOs.Questions
{
    public class CreateQuestionDto
    {
        public Guid QuizId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }
}
