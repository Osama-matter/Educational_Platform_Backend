using EducationalPlatform.Application.Interfaces.Security;
using BCryptNet = BCrypt.Net.BCrypt;

namespace EducationalPlatform.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCryptNet.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCryptNet.Verify(password, hashedPassword);
        }
    }
}
