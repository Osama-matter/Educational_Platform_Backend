using EducationalPlatform.Application.DTOs.QuizAttempt;
using EducationalPlatform.Application.DTOs.Answer;

namespace Educational_Platform_Front_End.Services.QuizAttempts
{
    public interface IQuizAttemptService
    {
        Task<Guid> CreateQuizAttemptAsync(CreateQuizAttemptDto createDto);
        Task<QuizAttemptDto> GetQuizAttemptByIdAsync(Guid attemptId);
        Task SubmitAnswersAsync(Guid attemptId, SubmitAnswersRequest answers);
    }
}
