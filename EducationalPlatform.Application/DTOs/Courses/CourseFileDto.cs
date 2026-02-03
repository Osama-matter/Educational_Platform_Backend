using System;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class CourseFileDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
