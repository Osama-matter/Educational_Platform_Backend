using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.External_services;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Domain.Entities.Course;
using EducationalPlatform.Infrastructure.Repositories;
using EducationalPlatform.Infrastructure.Security;
using EducationalPlatform.Infrastructure.Services;
using EducationalPlatform.Infrastructure.Services.External_services;
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
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ILessonProgressRepository, ProgressRepository>();
            services.AddScoped<IProgressService, ProgressServices>();

            return services;
        }
    }
}
