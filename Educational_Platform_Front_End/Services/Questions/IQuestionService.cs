using EducationalPlatform.Application.DTOs.Question;

namespace Educational_Platform_Front_End.Services.Questions
{
    public interface IQuestionService
    {
        Task<Guid> CreateQuestionAsync(CreateQuestionDto question);
        Task<List<QuestionDto>> GetQuestionsByQuizIdAsync(Guid quizId);
        Task DeleteQuestionAsync(Guid questionId);
    }
}
