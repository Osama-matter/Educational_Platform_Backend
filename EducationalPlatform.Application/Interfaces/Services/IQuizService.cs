using EducationalPlatform.Application.DTOs.Quiz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IQuizService
    {
        Task<QuizDto> GetQuizByIdAsync(Guid id);
        Task<IEnumerable<QuizDto>> GetQuizzesAsync();
        Task<Guid> CreateQuizAsync(CreateQuizDto createQuizDto);
        Task UpdateQuizAsync(Guid id, UpdateQuizDto updateQuizDto);
        Task DeleteQuizAsync(Guid id);
        Task<QuizDetailsDto> GetQuizDetailsForAdminAsync(Guid id);
    }
}