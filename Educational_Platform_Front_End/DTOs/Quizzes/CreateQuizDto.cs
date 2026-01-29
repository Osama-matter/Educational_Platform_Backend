namespace Educational_Platform_Front_End.DTOs.Quizzes
{
    public class CreateQuizDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public int TotalScore { get; set; }
        public int PassingScore { get; set; }
        public DateTime AvailableFrom { get; set; } = DateTime.UtcNow;
        public DateTime AvailableTo { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsPublished { get; set; } = false; // Default to not published
        public Guid LessonId { get; set; }
    }
}
