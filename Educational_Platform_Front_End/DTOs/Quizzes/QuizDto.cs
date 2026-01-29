namespace Educational_Platform_Front_End.DTOs.Quizzes
{
    public class QuizDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
    }
}
