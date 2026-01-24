using Educational_Platform_Front_End.Models.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public interface ICourseAdminService
    {
        Task<IReadOnlyList<CourseViewModel>> GetCoursesAsync(string token);
    }
}
