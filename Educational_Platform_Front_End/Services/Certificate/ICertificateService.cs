using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Educational_Platform_Front_End.DTOs.Certificate;

namespace Educational_Platform_Front_End.Services.Certificate
{
    public interface ICertificateService
    {
        Task<CertificateDto> IssueCertificateAsync(CreateCertificateDto createDto);
        Task<List<CertificateDto>> GetUserCertificatesAsync(Guid userId);
        Task<VerifyCertificateDto> VerifyCertificateAsync(string verificationCode);
        Task<bool> CertificateExistsAsync(Guid userId, Guid courseId);
        string GetDownloadUrl(Guid certificateId);
    }
}
