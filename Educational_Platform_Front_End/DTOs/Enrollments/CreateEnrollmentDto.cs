using System;


namespace EducationalPlatform.Application.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

 
    }
}
