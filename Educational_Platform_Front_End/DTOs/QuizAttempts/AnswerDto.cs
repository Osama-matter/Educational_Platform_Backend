namespace Educational_Platform_Front_End.DTOs.QuizAttempts
{
    public class AnswerDto
    {
        public Guid QuestionId { get; set; }
        public Guid SelectedOptionId { get; set; }
    }
}
