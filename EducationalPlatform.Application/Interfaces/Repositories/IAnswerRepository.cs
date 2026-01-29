using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        Task AddRangeAsync(IEnumerable<Answer> answers);
    }
}
