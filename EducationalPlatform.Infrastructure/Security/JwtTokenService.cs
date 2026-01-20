using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationalPlatform.Infrastructure.Security
{
    /// <summary>
    /// JWT Token generator service (Clean Architecture, best practices)
    /// </summary>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public JwtTokenService(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Generate JWT for a user with roles
        /// </summary>
        /// <param name="user">Identity user</param>
        /// <returns>JWT token string</returns>
        public async Task<string> GenerateTokenAsync(User user)
        {
            // Get roles assigned to user
            var roles = await _userManager.GetRolesAsync(user);

            // Base claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Symmetric key from configuration
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token expiry
            var minutes = _configuration["Jwt:AccessTokenMinutes"] ?? throw new InvalidOperationException("JWT AccessTokenMinutes not found.");
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(minutes));

            // Generate token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
