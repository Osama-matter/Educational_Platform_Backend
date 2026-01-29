using Educational_Platform_Front_End.DTOs.Quizzes;

namespace Educational_Platform_Front_End.DTOs.Courses
{
    public class CourseDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<LessonDto> Lessons { get; set; } = new();
    }
}
