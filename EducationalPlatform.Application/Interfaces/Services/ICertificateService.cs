using EducationalPlatform.Application.DTOs.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface ICertificateService
    {
        /// <summary>
        /// Issues a new certificate after verifying that the user has completed the course.
        /// </summary>
        /// <param name="createDto">The data required to create the certificate.</param>
        /// <returns>The issued certificate as a DTO.</returns>
        Task<CertificateDto> IssueCertificateAsync(CreateCertificateDto createDto);

        /// <summary>
        /// Retrieves all certificates that belong to a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>List of certificates as DTOs.</returns>
        Task<List<CertificateDto>> GetUserCertificatesAsync(Guid userId);

        /// <summary>
        /// Revokes an existing certificate.
        /// </summary>
        /// <param name="certificateId">The ID of the certificate to revoke.</param>
        /// <param name="reason">Optional reason for revocation.</param>
        Task RevokeCertificateAsync(Guid certificateId, string reason = null);

        /// <summary>
        /// Verifies a certificate using its unique verification code (for public checks).
        /// </summary>
        /// <param name="verificationCode">The verification code of the certificate.</param>
        /// <returns>Verification result as a DTO indicating if the certificate is valid.</returns>
        Task<VerifyCertificateDto> VerifyCertificateAsync(string verificationCode);

        /// <summary>
        /// Retrieves detailed information of a certificate by its ID.
        /// </summary>
        /// <param name="certificateId">The ID of the certificate.</param>
        /// <returns>Certificate details as a DTO.</returns>
        Task<CertificateDetailsDto> GetCertificateDetailsAsync(Guid certificateId);

        /// <summary>
        /// Checks if a certificate already exists for a specific user in a specific course.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns>True if the certificate exists, otherwise false.</returns>
        Task<bool> CertificateExistsAsync(Guid userId, Guid courseId);
        Task<(byte[] fileContents, string fileName)> DownloadCertificateAsync(Guid certificateId);
    }
}
