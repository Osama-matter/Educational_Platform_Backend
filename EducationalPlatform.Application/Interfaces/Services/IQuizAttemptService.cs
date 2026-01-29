using EducationalPlatform.Application.DTOs.QuizAttempt;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IQuizAttemptService
    {
        Task<QuizAttemptDto> GetQuizAttemptByIdAsync(Guid id);
        Task<IEnumerable<QuizAttemptDto>> GetQuizAttemptsAsync();
        Task<Guid> CreateQuizAttemptAsync(CreateQuizAttemptDto createQuizAttemptDto);
        Task UpdateQuizAttemptAsync(Guid id, UpdateQuizAttemptDto updateQuizAttemptDto);
        Task DeleteQuizAttemptAsync(Guid id);
    }
}
