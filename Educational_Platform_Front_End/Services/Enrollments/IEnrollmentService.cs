using System;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Enrollments
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResult> EnrollAsync(Guid courseId);
    }

    public class EnrollmentResult
    {
        public bool Success { get; set; }
        public string? PaymentUrl { get; set; }
        public string? Error { get; set; }
    }
}
