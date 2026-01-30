using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetByIdAsync(Guid id);
        Task<IEnumerable<Question>> GetByQuizIdAsync(Guid quizId);
        Task<IEnumerable<Question>> GetAllAsync();
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(Question question);
    }
}

