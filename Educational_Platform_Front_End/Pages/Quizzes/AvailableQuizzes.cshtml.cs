using EducationalPlatform.Application.DTOs.Quiz;
using EducationalPlatform.Application.DTOs.QuizAttempt;
using Educational_Platform_Front_End.Services.QuizAttempts;
using Educational_Platform_Front_End.Services.Quizzes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Quizzes
{
    public class AvailableQuizzesModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IQuizAttemptService _quizAttemptService;

        public AvailableQuizzesModel(IQuizService quizService, IQuizAttemptService quizAttemptService)
        {
            _quizService = quizService;
            _quizAttemptService = quizAttemptService;
        }

        public List<QuizDto> Quizzes { get; set; } = new();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var allQuizzes = await _quizService.GetAllQuizzesAsync();
                Quizzes = allQuizzes.Where(q => q.IsPublished).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load quizzes: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostStartAttemptAsync(Guid quizId)
        {
            try
            {
                var newAttemptId = await _quizAttemptService.CreateQuizAttemptAsync(new CreateQuizAttemptDto { QuizId = quizId });
                return RedirectToPage("./TakeQuiz", new { id = newAttemptId });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error starting quiz: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}
