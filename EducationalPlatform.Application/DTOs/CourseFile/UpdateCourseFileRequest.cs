using Microsoft.AspNetCore.Http;

namespace EducationalPlatform.Application.DTOs.CourseFile
{
    public class UpdateCourseFileRequest
    {
        public IFormFile File { get; set; }
    }
}

