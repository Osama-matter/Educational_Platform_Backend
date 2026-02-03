using EducationalPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetAllRewviewByCouseIDAsync(Guid  CourseId);
        Task AddAsync(Review entity);
        void Update(Review entity);
        void Delete(Review entity);
    }
}
