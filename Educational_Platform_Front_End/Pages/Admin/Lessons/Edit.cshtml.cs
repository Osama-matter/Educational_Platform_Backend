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
    public class EditModel : AdminPageModel
    {
        private readonly ILessonAdminService _lessonAdminService;
        private readonly ICourseAdminService _courseAdminService;

        public EditModel(
            IAdminAuthService adminAuthService,
            ILessonAdminService lessonAdminService,
            ICourseAdminService courseAdminService)
            : base(adminAuthService)
        {
            _lessonAdminService = lessonAdminService;
            _courseAdminService = courseAdminService;
        }

        [BindProperty]
        public LessonViewModel Lesson { get; set; }

        public IReadOnlyList<CourseViewModel> Courses { get; private set; } = new List<CourseViewModel>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            var token = GetToken();
            Lesson = await _lessonAdminService.GetLessonByIdAsync(token, id);
            if (Lesson == null)
            {
                return NotFound();
            }

            Courses = await _courseAdminService.GetCoursesAsync(token);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var guardResult = await RequireAdminAsync();
            if (guardResult != null)
            {
                return guardResult;
            }

            if (!ModelState.IsValid)
            {
                Courses = await _courseAdminService.GetCoursesAsync(GetToken());
                return Page();
            }

            var token = GetToken();
            var update = new UpdateLessonViewModel
            {
                CourseId = Lesson.CourseId,
                Title = Lesson.Title,
                Content = Lesson.Content,
                OrderIndex = Lesson.OrderIndex,
                DurationMinutes = Lesson.DurationMinutes
            };

            await _lessonAdminService.UpdateLessonAsync(token, id, update);
            return RedirectToPage("/Admin/Lessons/Index");
        }
    }
}
