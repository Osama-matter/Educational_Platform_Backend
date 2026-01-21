using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Enrollments
{
    public class CreateEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }

        public Enrollment ToEntity()
        {
            return new Enrollment(StudentId, CourseId);
        }
    }
}
