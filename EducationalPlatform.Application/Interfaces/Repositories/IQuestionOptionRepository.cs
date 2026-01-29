using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IQuestionOptionRepository
    {
        Task<QuestionOption> GetByIdAsync(Guid id);
        Task<IEnumerable<QuestionOption>> GetAllAsync();
        Task AddAsync(QuestionOption questionOption);
        Task UpdateAsync(QuestionOption questionOption);
        Task DeleteAsync(QuestionOption questionOption);
    }
}
