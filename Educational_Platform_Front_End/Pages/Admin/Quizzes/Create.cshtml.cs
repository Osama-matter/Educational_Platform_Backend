using EducationalPlatform.Application.DTOs.Quiz;
using Educational_Platform_Front_End.Services.Admin;
using Educational_Platform_Front_End.Services.Quizzes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Educational_Platform_Front_End.Pages.Admin.Quizzes
{
    public class CreateModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly ICourseAdminService _courseAdminService;
        private readonly ILessonAdminService _lessonAdminService;

        public CreateModel(IQuizService quizService, ICourseAdminService courseAdminService, ILessonAdminService lessonAdminService)
        {
            _quizService = quizService;
            _courseAdminService = courseAdminService;
            _lessonAdminService = lessonAdminService;
        }

        [BindProperty]
        public CreateQuizDto Quiz { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SelectedCourseId { get; set; }

        public SelectList Courses { get; set; }
        public SelectList Lessons { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var courses = await _courseAdminService.GetAllCoursesAsync();
            Courses = new SelectList(courses, "Id", "Title");

            if (!string.IsNullOrEmpty(SelectedCourseId) && Guid.TryParse(SelectedCourseId, out Guid courseId))
            {
                var lessons = await _lessonAdminService.GetLessonsForCourseAsync(courseId);
                Lessons = new SelectList(lessons, "Id", "Title");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                // Ensure LessonId is set if not already
                if (Quiz.LessonId == Guid.Empty && Request.Form.ContainsKey("Quiz.LessonId"))
                {
                    if (Guid.TryParse(Request.Form["Quiz.LessonId"], out Guid lessonId))
                    {
                        Quiz.LessonId = lessonId;
                    }
                }

                var newQuizId = await _quizService.CreateQuizAsync(Quiz);
                return RedirectToPage("./Build", new { id = newQuizId });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while creating the quiz: {ex.Message}";
                await OnGetAsync();
                return Page();
            }
        }
    }
}
