using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Enrollments
{
    public class EnrollmentDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public bool IsActive { get; set; }

        public EnrollmentDto()
        {
        }

        public EnrollmentDto(Enrollment enrollment)
        {
            Id = enrollment.Id;
            StudentId = enrollment.StudentId;
            CourseId = enrollment.CourseId;
            EnrolledAt = enrollment.EnrolledAt;
            IsActive = enrollment.IsActive;
        }
    }
}
