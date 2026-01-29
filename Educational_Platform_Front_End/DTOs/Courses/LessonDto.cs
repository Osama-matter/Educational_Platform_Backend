using Educational_Platform_Front_End.DTOs.QuizAttempts;
using Educational_Platform_Front_End.DTOs.Quizzes;

namespace Educational_Platform_Front_End.DTOs.Courses
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public QuizDto Quiz { get; set; }
        public List<QuizAttemptHistoryDto> QuizAttempts { get; set; } = new();
    }
}
