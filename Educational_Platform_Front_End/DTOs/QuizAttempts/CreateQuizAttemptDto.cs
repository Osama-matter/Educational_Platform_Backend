namespace Educational_Platform_Front_End.DTOs.QuizAttempts
{
    public class CreateQuizAttemptDto
    {
        public Guid QuizId { get; set; }
        // UserId will be added from claims in the backend or service layer
    }
}
