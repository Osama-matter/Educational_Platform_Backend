using Educational_Platform_Front_End.DTOs.Questions;

namespace Educational_Platform_Front_End.Services.Questions
{
    public interface IQuestionService
    {
        Task<Guid> CreateQuestionAsync(CreateQuestionDto question);
        Task DeleteQuestionAsync(Guid questionId);
    }
}
