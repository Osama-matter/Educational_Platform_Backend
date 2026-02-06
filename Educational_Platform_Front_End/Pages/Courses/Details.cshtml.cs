using EducationalPlatform.Application.DTOs.Courses;
using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Educational_Platform_Front_End.Services.Admin;
using Educational_Platform_Front_End.Services.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ILessonAdminService _lessonService;

        public DetailsModel(ICourseService courseService, ILessonAdminService lessonService)
        {
            _courseService = courseService;
            _lessonService = lessonService;
        }

        public CourseDetailsDto Course { get; set; }
        public IEnumerable<LessonViewModel> Lessons { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Course = await _courseService.GetCourseDetailsAsync(id);
                if (Course == null) return NotFound();

                var lessons = await _lessonService.GetLessonsForCourseAsync(id);
                Lessons = lessons.Cast<LessonViewModel>().OrderBy(l => l.OrderIndex);

                return Page();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
