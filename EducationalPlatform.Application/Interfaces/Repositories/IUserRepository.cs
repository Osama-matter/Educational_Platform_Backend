using EducationalPlatform.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
    }
}
