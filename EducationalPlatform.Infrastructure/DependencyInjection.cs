using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Domain.Entities.Course;
using EducationalPlatform.Infrastructure.Repositories;
using EducationalPlatform.Infrastructure.Security;
using EducationalPlatform.Infrastructure.Services;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NuGet.Protocol.Core.Types;

namespace EducationalPlatform.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ICourseService<CreateCourseDto, CourseDto>, CourseService<CreateCourseDto, CourseDto>>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            return services;
        }
    }
}
