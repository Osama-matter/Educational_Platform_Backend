using System;

namespace Educational_Platform_Front_End.DTOs.Certificate
{
    public class CertificateDto
    {
        public Guid Id { get; set; }
        public string CertificateNumber { get; set; }
        public string CourseTitle { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsRevoked { get; set; }
        public string DownloadUrl { get; set; }
    }

    public class CreateCertificateDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }

    public class VerifyCertificateDto
    {
        public string CertificateNumber { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsValid { get; set; }
    }
}
