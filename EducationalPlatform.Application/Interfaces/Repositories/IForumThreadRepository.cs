using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IForumThreadRepository
    {
        Task<ForumThread?> GetByIdAsync(Guid id);
        Task<IEnumerable<ForumThread>> GetAllAsync();
        Task AddAsync(ForumThread thread);
        Task UpdateAsync(ForumThread thread);
        Task DeleteAsync(Guid id);
    }
}
