using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CertificateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            await _dbContext.Certificates.AddAsync(certificate);
            await _dbContext.SaveChangesAsync();
            return certificate;
        }

        public async Task<Certificate> GetByIdAsync(Guid certificateId)
        {
            return await _dbContext.Certificates
                .Include(c => c.User)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.Id == certificateId);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid courseId)
        {
            return await _dbContext.Certificates
                .AnyAsync(c => c.UserId == userId && c.CourseId == courseId);
        }

        public async Task<List<Certificate>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Certificates
                .Where(c => c.UserId == userId)
                .Include(c => c.Course)
                .ToListAsync();
        }

        public async Task<Certificate> GetByVerificationCodeAsync(string verificationCode)
        {
            return await _dbContext.Certificates
                .Include(c => c.User)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.VerificationCode == verificationCode);
        }

        public async Task RevokeAsync(Certificate certificate)
        {
            certificate.Revoke();
            _dbContext.Certificates.Update(certificate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
