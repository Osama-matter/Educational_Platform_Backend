using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IQuizAttemptRepository
    {
        Task<QuizAttempt> GetByIdAsync(Guid id);
        Task<IEnumerable<QuizAttempt>> GetAllAsync();
        Task AddAsync(QuizAttempt quizAttempt);
        Task UpdateAsync(QuizAttempt quizAttempt);
        Task DeleteAsync(QuizAttempt quizAttempt );
        Task<IEnumerable<QuizAttempt>> GetByUserIdAndQuizIdAsync(Guid userId, Guid quizId);
    }
}

