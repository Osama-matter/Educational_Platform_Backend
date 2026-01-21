using EducationalPlatform.Application.Interfaces;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    internal class ProgressServices<TRequest, TResponse>(IRepository<Domain.Entities.LessonProgress> Progress_repository) : IProgressService<TRequest, TResponse>
    {
        IRepository<Domain.Entities.LessonProgress> _Progress_repository = Progress_repository;
        public Task<TResponse> CreateAsync(TRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> UpdateAsync(Guid id, TRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
