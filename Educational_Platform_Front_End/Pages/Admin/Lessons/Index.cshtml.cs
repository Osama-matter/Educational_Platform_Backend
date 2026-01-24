using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Educational_Platform_Front_End.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Admin.Lessons
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : AdminPageModel
    {
        private readonly ILessonAdminService _lessonAdminService;
        private readonly ICourseAdminService _courseAdminService;

        public IndexModel(
            IAdminAuthService adminAuthService,
            ILessonAdminService lessonAdminService,
            ICourseAdminService courseAdminService)
            : base(adminAuthService)
        {
            _lessonAdminService = lessonAdminService;
            _courseAdminService = courseAdminService;
        }

        public IReadOnlyList<LessonViewModel> Lessons { get; private set; } = new List<LessonViewModel>();
        public IReadOnlyList<CourseViewModel> Courses { get; private set; } = new List<CourseViewModel>();
        public Dictionary<Guid, string> CourseLookup => Courses.ToDictionary(course => course.Id, course => course.Title);

        public async Task<IActionResult> OnGetAsync()
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            var token = GetToken();
            Courses = await _courseAdminService.GetCoursesAsync(token);
            Lessons = await _lessonAdminService.GetLessonsAsync(token);
            Lessons = Lessons.OrderBy(lesson => lesson.CourseId)
                .ThenBy(lesson => lesson.OrderIndex)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            var token = GetToken();
            await _lessonAdminService.DeleteLessonAsync(token, id);

            return RedirectToPage();
        }
    }
}
