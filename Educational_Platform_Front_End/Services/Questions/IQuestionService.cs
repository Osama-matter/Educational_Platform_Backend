using EducationalPlatform.Application.DTOs.Question;

namespace Educational_Platform_Front_End.Services.Questions
{
    public interface IQuestionService
    {
        Task<Guid> CreateQuestionAsync(CreateQuestionDto question);
        Task DeleteQuestionAsync(Guid questionId);
    }
}
