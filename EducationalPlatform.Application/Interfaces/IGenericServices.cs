using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces
{
    public interface IGenericServices<TRequest , TResponse>
    {

        public  Task<TResponse> CreateAsync(TRequest request);
        public  Task<TResponse> GetByIdAsync(Guid id);
        public  Task<IEnumerable<TResponse>> GetAllAsync();
        public  Task<TResponse> UpdateAsync(Guid id, TRequest request);
        public  Task<bool> DeleteAsync(Guid id);
    }
}
