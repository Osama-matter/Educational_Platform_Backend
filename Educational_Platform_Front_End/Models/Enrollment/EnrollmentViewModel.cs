using Educational_Platform_Front_End.Models.Courses;
using System;

namespace Educational_Platform_Front_End.Models.Enrollment
{
    public class EnrollmentViewModel
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime? EnrolledAt { get; set; }
        public CourseViewModel Course { get; set; }
        public double Progress { get; set; }
        public bool IsActive { get; set; }
        public string? PaymentStatus { get; set; }
    }
}
