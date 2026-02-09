using EducationalPlatform.Application.DTOs.Courses;
using Educational_Platform_Front_End.Models.Courses;
using Educational_Platform_Front_End.Models.Lessons;
using Educational_Platform_Front_End.Services.Admin;
using Educational_Platform_Front_End.Services.Courses;
using Educational_Platform_Front_End.Services.Enrollments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_Platform_Front_End.Services.Reviews;
using EducationalPlatform.Application.DTOs.Review;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly ILessonAdminService _lessonService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IReviewService _reviewService;

        public DetailsModel(
            ICourseService courseService, 
            ILessonAdminService lessonService, 
            IEnrollmentService enrollmentService,
            IReviewService reviewService)
        {
            _courseService = courseService;
            _lessonService = lessonService;
            _enrollmentService = enrollmentService;
            _reviewService = reviewService;
        }

        public CourseDetailsDto Course { get; set; }
        public IEnumerable<LessonViewModel> Lessons { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new();

        [BindProperty]
        public CreateReviewDto NewReview { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Course = await _courseService.GetCourseDetailsAsync(id);
                if (Course == null) return NotFound();

                var lessons = await _lessonService.GetLessonsForCourseAsync(id);
                Lessons = lessons.Cast<LessonViewModel>().OrderBy(l => l.OrderIndex);

                Reviews = await _reviewService.GetReviewsForCourseAsync(id);

                return Page();
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostRateAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync(id);
                return Page();
            }

            NewReview.CourseId = id;
            await _reviewService.CreateReviewAsync(NewReview);
            return RedirectToPage(new { id });
        }

        private async Task LoadDataAsync(Guid id)
        {
            Course = await _courseService.GetCourseDetailsAsync(id);
            var lessons = await _lessonService.GetLessonsForCourseAsync(id);
            Lessons = lessons.Cast<LessonViewModel>().OrderBy(l => l.OrderIndex);
            Reviews = await _reviewService.GetReviewsForCourseAsync(id);
        }

        public async Task<IActionResult> OnPostEnrollAsync(Guid id)
        {
            var result = await _enrollmentService.EnrollAsync(id);
            if (result.Success && !string.IsNullOrEmpty(result.PaymentUrl))
            {
                return Redirect(result.PaymentUrl);
            }

            ModelState.AddModelError(string.Empty, result.Error ?? "Enrollment failed.");
            
            await LoadDataAsync(id);
            
            return Page();
        }
    }
}
