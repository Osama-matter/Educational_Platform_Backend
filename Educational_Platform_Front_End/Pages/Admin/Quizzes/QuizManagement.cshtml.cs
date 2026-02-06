
using Educational_Platform_Front_End.Services.Quizzes;
using EducationalPlatform.Application.DTOs.Quiz;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Admin.Quizzes
{
    public class QuizManagementModel : PageModel
    {
        private readonly IQuizService _quizService;

        public QuizManagementModel(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public List<QuizDto> Quizzes { get; set; } = new();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                Quizzes = await _quizService.GetAllQuizzesAsync();
            }
            catch (System.Exception ex)
            {
                ErrorMessage = $"Failed to load quizzes: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _quizService.DeleteQuizAsync(id);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to delete quiz: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}
