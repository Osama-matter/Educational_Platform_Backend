using EducationalPlatform.Application.DTOs.Question;
using EducationalPlatform.Application.DTOs.QuestionOption;
using EducationalPlatform.Application.DTOs.Quiz;
using Educational_Platform_Front_End.Services.Questions;
using Educational_Platform_Front_End.Services.Quizzes;
using EducationalPlatform.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Admin.Quizzes
{
    public class BuildModel : PageModel
    {
        private readonly IQuizService _quizService;
        private readonly IQuestionService _questionService;
        private readonly IQuestionOptionService _questionOptionService;

        public BuildModel(IQuizService quizService, IQuestionService questionService, IQuestionOptionService questionOptionService)
        {
            _quizService = quizService;
            _questionService = questionService;
            _questionOptionService = questionOptionService;
        }

        [BindProperty]
        [ValidateNever]
        public QuizDetailsDto Quiz { get; set; } = new QuizDetailsDto();

        [BindProperty]
        public CreateQuestionDto NewQuestion { get; set; } = new CreateQuestionDto();

        [BindProperty]
        public CreateQuestionOptionDto NewOption { get; set; } = new CreateQuestionOptionDto();

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            await LoadQuizData(id);
            if (Quiz == null) return RedirectToPage("./QuizManagement");
            return Page();
        }

        public async Task<IActionResult> OnPostAddQuestionAsync(Guid id)
        {
            if (NewQuestion == null)
            {
                ErrorMessage = "Question data was not received by the server.";
                await LoadQuizData(id);
                return Page();
            }

            // Remove validation for properties we set manually or that belong to other models
            ModelState.Remove("NewQuestion.QuizId");
            
            // Clear errors for other properties on the page that aren't part of this action
            foreach (var key in ModelState.Keys.Where(k => !k.StartsWith("NewQuestion")).ToList())
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                ErrorMessage = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                await LoadQuizData(id);
                return Page();
            }
            try
            {
                if (NewQuestion.QuestionType == default)
                {
                    NewQuestion.QuestionType = QuestionType.MultipleChoice;
                }
                NewQuestion.QuizId = id;
                await _questionService.CreateQuestionAsync(NewQuestion);
                return RedirectToPage(new { id });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding question: {ex.Message}";
                await LoadQuizData(id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteQuestionAsync(Guid id, Guid questionId)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(questionId);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting question: {ex.Message}";
            }
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostAddOptionAsync(Guid id, Guid questionId)
        {
            // Remove validation for properties we set manually
            ModelState.Remove("NewOption.QuestionId");

            // Clear errors for other properties on the page that aren't part of this action
            foreach (var key in ModelState.Keys.Where(k => !k.StartsWith("NewOption")).ToList())
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                ErrorMessage = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                await LoadQuizData(id);
                return Page();
            }

            try
            {
                NewOption.QuestionId = questionId;
                await _questionOptionService.CreateQuestionOptionAsync(NewOption);
                return RedirectToPage(new { id });
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding option: {ex.Message}";
                await LoadQuizData(id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteOptionAsync(Guid id, Guid optionId)
        {
            try
            {
                await _questionOptionService.DeleteQuestionOptionAsync(optionId);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error deleting option: {ex.Message}";
            }
            return RedirectToPage(new { id });
        }

        private async Task LoadQuizData(Guid id)
        {
            try
            {
                Quiz = await _quizService.GetQuizDetailsForAdminAsync(id);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading quiz data: {ex.Message}";
            }
        }
    }
}
