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
        public Guid SelectedCourseId { get; set; }

        public SelectList Courses { get; set; }
        public SelectList Lessons { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var courses = await _courseAdminService.GetAllCoursesAsync();
            Courses = new SelectList(courses, "Id", "Title");

            if (SelectedCourseId != Guid.Empty)
            {
                var lessons = await _lessonAdminService.GetLessonsForCourseAsync(SelectedCourseId);
                Lessons = new SelectList(lessons, "Id", "Title");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Reload dropdowns
                return Page();
            }

            try
            {
                var newQuizId = await _quizService.CreateQuizAsync(Quiz);
                return RedirectToPage("./Build", new { id = newQuizId });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while creating the quiz: {ex.Message}";
                await OnGetAsync(); // Reload dropdowns
                return Page();
            }
        }
    }
}
