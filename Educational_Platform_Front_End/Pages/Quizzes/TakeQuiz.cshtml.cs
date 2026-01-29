using Educational_Platform_Front_End.DTOs.QuizAttempts;
using Educational_Platform_Front_End.Services.QuizAttempts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Quizzes
{
    public class TakeQuizModel : PageModel
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public TakeQuizModel(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        public QuizAttemptDto QuizAttempt { get; set; }

        [BindProperty]
        public Dictionary<Guid, Guid> SelectedAnswers { get; set; } = new();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                QuizAttempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
                if (QuizAttempt == null) return NotFound();

                foreach (var question in QuizAttempt.Questions)
                {
                    SelectedAnswers.Add(question.Id, Guid.Empty);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading quiz: {ex.Message}";
                return RedirectToPage("./AvailableQuizzes");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                QuizAttempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
                return Page();
            }

            try
            {
                var submission = new SubmitAnswersRequest
                {
                    Answers = SelectedAnswers.Select(kvp => new AnswerDto { QuestionId = kvp.Key, SelectedOptionId = kvp.Value }).ToList()
                };

                await _quizAttemptService.SubmitAnswersAsync(id, submission);

                return RedirectToPage("./QuizResult", new { id });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error submitting quiz: {ex.Message}";
                QuizAttempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
                return Page();
            }
        }
    }
}
