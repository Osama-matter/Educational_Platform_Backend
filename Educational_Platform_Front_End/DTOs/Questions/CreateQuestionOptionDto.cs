namespace Educational_Platform_Front_End.DTOs.Questions
{
    public class CreateQuestionOptionDto
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
