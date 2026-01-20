using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace EducationalPlatform.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
