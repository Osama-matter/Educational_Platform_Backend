using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IForumSubscriptionRepository
    {
        Task<ForumSubscriptions?> GetByIdAsync(Guid id);
        Task<IEnumerable<ForumSubscriptions>> GetByUserIdAsync(Guid userId);
        Task<ForumSubscriptions?> GetByUserAndThreadAsync(Guid userId, Guid threadId);
        Task AddAsync(ForumSubscriptions subscription);
        Task DeleteAsync(Guid id);
    }
}
