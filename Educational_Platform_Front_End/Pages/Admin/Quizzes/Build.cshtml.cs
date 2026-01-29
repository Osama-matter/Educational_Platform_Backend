using Educational_Platform_Front_End.DTOs.Questions;
using Educational_Platform_Front_End.DTOs.Quizzes.Admin;
using Educational_Platform_Front_End.Services.Questions;
using Educational_Platform_Front_End.Services.Quizzes;
using Microsoft.AspNetCore.Mvc;
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
        public QuizDetailsDto Quiz { get; set; }

        [BindProperty]
        public CreateQuestionDto NewQuestion { get; set; }

        [BindProperty]
        public CreateQuestionOptionDto NewOption { get; set; }

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
            if (!ModelState.IsValid)
            {
                await LoadQuizData(id);
                return Page();
            }
            try
            {
                NewQuestion.QuizId = id;
                await _questionService.CreateQuestionAsync(NewQuestion);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding question: {ex.Message}";
            }
            return RedirectToPage(new { id });
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
            try
            {
                NewOption.QuestionId = questionId;
                await _questionOptionService.CreateQuestionOptionAsync(NewOption);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding option: {ex.Message}";
            }
            return RedirectToPage(new { id });
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
