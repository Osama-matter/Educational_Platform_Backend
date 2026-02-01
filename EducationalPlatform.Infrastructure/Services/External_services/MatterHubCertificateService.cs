using EducationalPlatform.Application.DTOs.Certificate;
using EducationalPlatform.Application.Interfaces.External_services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services.External_services
{

    /// <summary>
    /// MatterHub Certificate Service
    /// Handles certificate generation, validation, and PDF creation
    /// </summary>
    public class MatterHubCertificateService
    {
        private readonly IMatterHubCertificateGenerator _pdfGenerator;
        private readonly ILogger<MatterHubCertificateService> _logger;

        public MatterHubCertificateService(
            IMatterHubCertificateGenerator pdfGenerator,
            ILogger<MatterHubCertificateService> logger)
        {
            _pdfGenerator = pdfGenerator ?? throw new ArgumentNullException(nameof(pdfGenerator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates a MatterHub certificate PDF
        /// </summary>
        /// <param name="userFullName">Student's full name</param>
        /// <param name="courseTitle">Course title</param>
        /// <param name="instructorName">Optional instructor name</param>
        /// <param name="logoPath">Optional MatterHub logo path</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Certificate DTO with PDF file path</returns>
        public async Task<CertificateDetailsDto> GenerateCertificateAsync(
            string userFullName,
            string courseTitle,
            string? instructorName = null,
            string? logoPath = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Generating MatterHub certificate for {UserName}, Course: {CourseTitle}",
                    userFullName,
                    courseTitle);

                // Validate inputs
                ValidateInput(userFullName, courseTitle);

                // Generate certificate number and verification code
                var certificateNumber = GenerateCertificateNumber();
                var verificationCode = GenerateVerificationCode();

                // Determine file path
                var pdfFilePath = GeneratePdfFilePath(userFullName, certificateNumber);

                // Create DTO
                var certificateDto = new CertificateDetailsDto
                {
                    UserFullName = userFullName,
                    CourseTitle = courseTitle,
                    CertificateNumber = certificateNumber,
                    VerificationCode = verificationCode,
                    IssuedAt = DateTime.UtcNow,
                    PdfFilePath = pdfFilePath,
                    InstructorName = instructorName,
                    LogoPath = logoPath,
                    VerificationUrl = GenerateVerificationUrl(verificationCode)
                };

                // Generate PDF
                var generatedPath = await _pdfGenerator.GenerateCertificatePdfAsync(
                    certificateDto,
                    cancellationToken);

                _logger.LogInformation(
                    "Certificate generated successfully: {CertificateNumber} at {FilePath}",
                    certificateNumber,
                    generatedPath);

                return certificateDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error generating certificate for {UserName}",
                    userFullName);
                throw;
            }
        }

        /// <summary>
        /// Generates a certificate and returns it as byte array (for downloads)
        /// </summary>
        public async Task<byte[]> GenerateCertificateBytesAsync(
            string userFullName,
            string courseTitle,
            string? instructorName = null,
            string? logoPath = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation(
                    "Generating MatterHub certificate bytes for {UserName}",
                    userFullName);

                // Validate inputs
                ValidateInput(userFullName, courseTitle);

                // Generate codes once and reuse for both DTO and URL
                var verificationCode = GenerateVerificationCode();

                // Create DTO
                var certificateDto = new CertificateDetailsDto
                {
                    UserFullName = userFullName,
                    CourseTitle = courseTitle,
                    CertificateNumber = GenerateCertificateNumber(),
                    VerificationCode = verificationCode,
                    IssuedAt = DateTime.UtcNow,
                    PdfFilePath = "temp.pdf", // Not used for byte generation
                    InstructorName = instructorName,
                    LogoPath = logoPath,
                    VerificationUrl = GenerateVerificationUrl(verificationCode) // uses the same code
                };

                // Generate PDF as bytes
                var pdfBytes = await _pdfGenerator.GenerateCertificatePdfBytesAsync(
                    certificateDto,
                    cancellationToken);

                _logger.LogInformation(
                    "Certificate bytes generated: {Size} bytes for {UserName}",
                    pdfBytes.Length,
                    userFullName);

                return pdfBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error generating certificate bytes for {UserName}",
                    userFullName);
                throw;
            }
        }

        /// <summary>
        /// Batch generate multiple certificates
        /// </summary>
        public async Task<List<CertificateDetailsDto>> GenerateBatchCertificatesAsync(
            List<(string UserName, string CourseTitle)> students,
            string? instructorName = null,
            string? logoPath = null,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Starting batch generation of {Count} certificates",
                students.Count);

            var certificates = new List<CertificateDetailsDto>();

            foreach (var student in students)
            {
                var cert = await GenerateCertificateAsync(
                    student.UserName,
                    student.CourseTitle,
                    instructorName,
                    logoPath,
                    cancellationToken);

                certificates.Add(cert);
            }

            _logger.LogInformation(
                "Batch generation completed: {Count} certificates",
                certificates.Count);

            return certificates;
        }

        #region Private Helper Methods

        /// <summary>
        /// Validates input parameters
        /// </summary>
        private void ValidateInput(string userFullName, string courseTitle)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(userFullName))
                errors.Add("User full name is required");

            if (string.IsNullOrWhiteSpace(courseTitle))
                errors.Add("Course title is required");

            if (userFullName?.Length > 100)
                errors.Add("User full name is too long (max 100 characters)");

            if (courseTitle?.Length > 200)
                errors.Add("Course title is too long (max 200 characters)");

            if (errors.Any())
            {
                throw new ArgumentException(
                    $"Certificate validation failed: {string.Join(", ", errors)}");
            }
        }

        /// <summary>
        /// Generates unique MatterHub certificate number
        /// Format: MH-YYYY-XXXXXX
        /// </summary>
        private string GenerateCertificateNumber()
        {
            var year = DateTime.UtcNow.Year;
            var uniqueId = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"MH-{year}-{uniqueId}";
        }

        /// <summary>
        /// Generates verification code
        /// Format: 12-character alphanumeric
        /// </summary>
        private string GenerateVerificationCode()
        {
            return Guid.NewGuid().ToString("N")[..12].ToUpper();
        }

        /// <summary>
        /// Generates PDF file path with proper naming
        /// </summary>
        private string GeneratePdfFilePath(string userName, string certificateNumber)
        {
            // Sanitize username for file name
            var safeUserName = string.Join("_",
                userName.Split(Path.GetInvalidFileNameChars()));

            // Create filename
            var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
            var fileName = $"MatterHub_Certificate_{safeUserName}_{timestamp}.pdf";

            // Ensure Certificates directory exists
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "Certificates", "MatterHub");
            Directory.CreateDirectory(directory);

            return Path.Combine(directory, fileName);
        }

        /// <summary>
        /// Generates verification URL
        /// </summary>
        private string GenerateVerificationUrl(string verificationCode)
        {
            // In production, use your actual domain
            return $"https://matterhub.com/verify/{verificationCode}";
        }

        #endregion
    }
}