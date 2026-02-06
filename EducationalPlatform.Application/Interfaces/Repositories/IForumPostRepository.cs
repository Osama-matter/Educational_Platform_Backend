using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IForumPostRepository
    {
        Task<ForumPost?> GetByIdAsync(Guid id);
        Task<IEnumerable<ForumPost>> GetByThreadIdAsync(Guid threadId);
        Task AddAsync(ForumPost post);
        Task UpdateAsync(ForumPost post);
        Task DeleteAsync(Guid id);
    }
}
