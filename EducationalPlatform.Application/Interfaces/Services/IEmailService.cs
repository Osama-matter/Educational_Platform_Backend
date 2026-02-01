using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string to, string userName);
        Task SendEnrollmentEmailAsync(string to, string userName, string courseName);
        Task SendCertificateEmailAsync(string to, string userName, string courseName, byte[] certificate);
        Task SendWeeklyDigestAsync(string to, string userName, string digestContent);
    }
}
