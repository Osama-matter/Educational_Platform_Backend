using EducationalPlatform.Application.DTOs.QuizAttempt;
using EducationalPlatform.Application.DTOs.Answer;
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

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public List<Guid> QuestionIds { get; set; } = new();

        [BindProperty]
        public List<Guid> AnswerIds { get; set; } = new();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                QuizAttempt = await _quizAttemptService.GetQuizAttemptByIdAsync(id);
                if (QuizAttempt == null) return NotFound();

                QuestionIds = QuizAttempt.Questions.Select(q => q.Id).ToList();
                AnswerIds = QuizAttempt.Questions.Select(_ => Guid.Empty).ToList();
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
                    Answers = new List<AnswerDto>()
                };

                for (int i = 0; i < QuestionIds.Count; i++)
                {
                    submission.Answers.Add(new AnswerDto 
                    { 
                        QuestionId = QuestionIds[i], 
                        SelectedOptionId = AnswerIds[i] 
                    });
                }

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
