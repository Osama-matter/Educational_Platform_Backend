using Educational_Platform_Front_End.DTOs.Courses;

namespace Educational_Platform_Front_End.Services.Courses
{
    public interface ICourseService
    {
        Task<CourseDetailsDto> GetCourseDetailsAsync(Guid courseId);
    }
}
