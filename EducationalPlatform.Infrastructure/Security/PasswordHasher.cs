using EducationalPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EducationalPlatform.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return BCryptNet.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            if (BCryptNet.Verify(providedPassword, hashedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}
