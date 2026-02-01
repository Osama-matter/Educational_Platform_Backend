using EducationalPlatform.Application.DTOs.Enrollments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IEnrollmentService
    {
        Task<EnrollmentDto> CreateAsync(Guid studentId, Guid courseId);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<EnrollmentDto>> GetAllAsync();
        Task<EnrollmentDto> GetByIdAsync(Guid id);
        Task<EnrollmentDto> UpdateAsync(Guid id, UpdateEnrollmentDto request);
    }
}
