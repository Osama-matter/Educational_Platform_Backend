using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Educational_Platform_Front_End.Services.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Admin.Lessons
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : AdminPageModel
    {
        private readonly ILessonAdminService _lessonAdminService;
        private readonly ICourseAdminService _courseAdminService;

        public CreateModel(
            IAdminAuthService adminAuthService,
            ILessonAdminService lessonAdminService,
            ICourseAdminService courseAdminService)
            : base(adminAuthService)
        {
            _lessonAdminService = lessonAdminService;
            _courseAdminService = courseAdminService;
        }

        [BindProperty]
        public CreateLessonViewModel Lesson { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid? CourseId { get; set; }

        public IReadOnlyList<CourseViewModel> Courses { get; private set; } = new List<CourseViewModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            await LoadCoursesAsync();
            if (CourseId.HasValue)
            {
                Lesson.CourseId = CourseId.Value;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            if (!ModelState.IsValid)
            {
                await LoadCoursesAsync();
                return Page();
            }

            var token = GetToken();
            await _lessonAdminService.CreateLessonAsync(token, Lesson);
            return RedirectToPage("/Admin/Lessons/Index");
        }

        private async Task LoadCoursesAsync()
        {
            var token = GetToken();
            Courses = await _courseAdminService.GetCoursesAsync(token);
        }
    }
}
