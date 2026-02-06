using EducationalPlatform.Application.DTOs.Quiz;
using Educational_Platform_Front_End.Services.Quizzes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Admin.Quizzes
{
    public class ReviewModel : PageModel
    {
        private readonly IQuizService _quizService;

        public ReviewModel(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public QuizDetailsDto Quiz { get; set; }
        public List<string> ValidationErrors { get; set; } = new();
        public bool IsValidForPublishing => !ValidationErrors.Any();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await LoadAndValidateQuiz(id);
            if (Quiz == null) return RedirectToPage("./QuizManagement");
            return Page();
        }

        public async Task<IActionResult> OnPostPublishAsync(Guid id)
        {
            await LoadAndValidateQuiz(id);
            if (!IsValidForPublishing)
            {
                ErrorMessage = "Cannot publish a quiz that has validation errors.";
                return Page();
            }

            try
            {
                await _quizService.PublishQuizAsync(id);
                return RedirectToPage("./QuizManagement");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while publishing the quiz: {ex.Message}";
                return Page();
            }
        }

        private async Task LoadAndValidateQuiz(Guid id)
        {
            try
            {
                Quiz = await _quizService.GetQuizDetailsForAdminAsync(id);
                if (Quiz == null) return;

                // Perform validation
                if (!Quiz.Questions.Any())
                {
                    ValidationErrors.Add("The quiz must have at least one question.");
                }

                foreach (var question in Quiz.Questions)
                {
                    if (question.Options.Count < 2)
                    {
                        ValidationErrors.Add($"Question '{question.Text}' must have at least two answer options.");
                    }

                    var correctOptionsCount = question.Options.Count(o => o.IsCorrect);
                    if (correctOptionsCount != 1)
                    {
                        ValidationErrors.Add($"Question '{question.Text}' must have exactly one correct answer. It currently has {correctOptionsCount}.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading quiz data: {ex.Message}";
            }
        }
    }
}
