using Educational_Platform_Front_End.DTOs.Questions;

namespace Educational_Platform_Front_End.Services.Questions
{
    public interface IQuestionOptionService
    {
        Task CreateQuestionOptionAsync(CreateQuestionOptionDto option);
        Task DeleteQuestionOptionAsync(Guid optionId);
    }
}
