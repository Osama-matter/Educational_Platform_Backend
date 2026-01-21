using System;
using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.DTOs.Enrollments
{
    public class UpdateEnrollmentDto
    {
        public bool IsActive { get; set; }

        public void ApplyTo(Enrollment enrollment)
        {
            enrollment.IsActive = IsActive;
        }
    }
}
