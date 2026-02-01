using EducationalPlatform.Application.DTOs.Certificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.External_services
{
    // <summary>
    /// Interface for MatterHub certificate PDF generation
    /// </summary>
    public interface IMatterHubCertificateGenerator
    {
        /// <summary>
        /// Generates a MatterHub certificate PDF and saves it to the specified path
        /// </summary>
        /// <param name="certificateDto">Certificate data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The file path where the PDF was saved</returns>
        Task<string> GenerateCertificatePdfAsync(
            CertificateDetailsDto certificateDto,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Generates a MatterHub certificate PDF and returns it as byte array
        /// </summary>
        /// <param name="certificateDto">Certificate data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>PDF file as byte array</returns>
        Task<byte[]> GenerateCertificatePdfBytesAsync(
            CertificateDetailsDto certificateDto,
            CancellationToken cancellationToken = default);
    }
}

