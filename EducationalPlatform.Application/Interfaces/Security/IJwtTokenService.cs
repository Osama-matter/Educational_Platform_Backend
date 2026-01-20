using EducationalPlatform.Domain.Entities;

namespace EducationalPlatform.Application.Interfaces.Security
{
    /// <summary>
    /// JWT Token generator interface
    /// </summary>
    public interface IJwtTokenService
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
