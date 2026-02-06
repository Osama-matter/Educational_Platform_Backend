namespace Educational_Platform_Front_End.Models.Lessons
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int OrderIndex { get; set; }
        public int? DurationMinutes { get; set; }
        public List<EducationalPlatform.Application.DTOs.Courses.QuizSummaryDto> Quizzes { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status => string.IsNullOrWhiteSpace(Content) ? "Draft" : "Published";
    }
}
