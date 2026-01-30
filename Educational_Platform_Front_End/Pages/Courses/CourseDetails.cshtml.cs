using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.DTOs.QuizAttempt;
using Educational_Platform_Front_End.Services.Courses;
using Educational_Platform_Front_End.Services.QuizAttempts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Courses
{
    public class CourseDetailsModel : PageModel
    {
        private readonly ICourseService _courseService;
        private readonly IQuizAttemptService _quizAttemptService;

        public CourseDetailsModel(ICourseService courseService, IQuizAttemptService quizAttemptService)
        {
            _courseService = courseService;
            _quizAttemptService = quizAttemptService;
        }

        public CourseDetailsDto Course { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Course = await _courseService.GetCourseDetailsAsync(id);
                if (Course == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading course details: {ex.Message}";
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostStartQuizAsync(Guid quizId)
        {
            try
            {
                var newAttemptId = await _quizAttemptService.CreateQuizAttemptAsync(new CreateQuizAttemptDto { QuizId = quizId });
                return RedirectToPage("../Quizzes/TakeQuiz", new { id = newAttemptId });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error starting quiz: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}
