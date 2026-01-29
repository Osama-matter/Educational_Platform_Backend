namespace Educational_Platform_Front_End.DTOs.QuizAttempts
{
    public class QuizAttemptHistoryDto
    {
        public Guid Id { get; set; }
        public int TotalScore { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
