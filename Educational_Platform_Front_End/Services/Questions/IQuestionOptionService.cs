using EducationalPlatform.Application.DTOs.QuestionOption;

namespace Educational_Platform_Front_End.Services.Questions
{
    public interface IQuestionOptionService
    {
        Task CreateQuestionOptionAsync(CreateQuestionOptionDto option);
        Task DeleteQuestionOptionAsync(Guid optionId);
    }
}
