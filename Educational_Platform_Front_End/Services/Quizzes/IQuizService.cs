using EducationalPlatform.Application.DTOs.Quiz;

namespace Educational_Platform_Front_End.Services.Quizzes
{
    public interface IQuizService
    {
        Task<List<QuizDto>> GetAllQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(Guid quizId);
        Task<Guid> CreateQuizAsync(CreateQuizDto quiz);
        Task<QuizDetailsDto> GetQuizDetailsForAdminAsync(Guid quizId);
        Task UpdateQuizAsync(Guid quizId, QuizDto quiz);
        Task DeleteQuizAsync(Guid quizId);
        Task PublishQuizAsync(Guid quizId);
    }
}
