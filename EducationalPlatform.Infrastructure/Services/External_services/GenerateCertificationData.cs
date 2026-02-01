using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services.External_services
{
    public class GenerateCertificationData
    {
        public static string GenerateCertificateNumber()
        {
            // Generate a unique certificate number (e.g., using GUID)
            return Guid.NewGuid().ToString().ToUpper();
        }

        public static string GenerateVerificationCode()
        {
            // Generate a unique verification code (e.g., using a random alphanumeric string)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
