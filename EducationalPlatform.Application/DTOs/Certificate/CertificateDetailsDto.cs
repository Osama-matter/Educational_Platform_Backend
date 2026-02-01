using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Certificate
{
    public class CertificateDetailsDto
    {
        /// <summary>
        /// Full name of the student
        /// </summary>
        public string UserFullName { get; set; } = string.Empty;

        /// <summary>
        /// Title of the completed course
        /// </summary>
        public string CourseTitle { get; set; } = string.Empty;

        /// <summary>
        /// Unique certificate number (e.g., MH-2024-ABC123)
        /// </summary>
        public string CertificateNumber { get; set; } = string.Empty;

        /// <summary>
        /// Unique verification code
        /// </summary>
        public string VerificationCode { get; set; } = string.Empty;

        /// <summary>
        /// Date when the certificate was issued
        /// </summary>
        public DateTime IssuedAt { get; set; }

        /// <summary>
        /// File path where the PDF should be saved
        /// </summary>
        public string PdfFilePath { get; set; } = string.Empty;

        /// <summary>
        /// Optional: Instructor name for signature
        /// </summary>
        public string? InstructorName { get; set; }

        /// <summary>
        /// Optional: Verification URL for QR code
        /// </summary>
        public string? VerificationUrl { get; set; }

        /// <summary>
        /// Optional: MatterHub logo path
        /// </summary>
        public string? LogoPath { get; set; }
        public bool IsRevoked { get; set; }
    }
}
