using EducationalPlatform.Application.DTOs.QuizAttempt;
using Educational_Platform_Front_End.Services.QuizAttempts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Quizzes
{
    public class QuizResultModel : PageModel
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizResultModel(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        public QuizAttemptDto QuizAttempt { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                QuizAttempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
                if (QuizAttempt == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading quiz result: {ex.Message}";
                return RedirectToPage("./AvailableQuizzes");
            }
            return Page();
        }
    }
}
