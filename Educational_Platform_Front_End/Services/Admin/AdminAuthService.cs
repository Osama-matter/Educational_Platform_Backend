using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public class AdminAuthService : IAdminAuthService
    {
        public Task<bool> IsAdminAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Task.FromResult(false);
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var roles = jwt.Claims
                    .Where(claim => claim.Type.EndsWith("/role", StringComparison.OrdinalIgnoreCase) ||
                                    claim.Type.Equals("role", StringComparison.OrdinalIgnoreCase) ||
                                    claim.Type.Equals("roles", StringComparison.OrdinalIgnoreCase))
                    .Select(claim => claim.Value);

                return Task.FromResult(roles.Any(role => string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase)));
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
