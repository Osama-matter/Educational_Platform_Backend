using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Repositories
{
    public interface ICertificateRepository
    {
        // إنشاء شهادة جديدة
        Task<Certificate> AddAsync(Certificate certificate);

        // الحصول على شهادة بواسطة Id
        Task<Certificate> GetByIdAsync(Guid certificateId);

        // التحقق إذا المستخدم حصل على الشهادة مسبقًا للكورس
        Task<bool> ExistsAsync(Guid userId, Guid courseId);

        // الحصول على جميع الشهادات الخاصة بمستخدم
        Task<List<Certificate>> GetByUserIdAsync(Guid userId);

        // حفظ أي تغييرات
        Task SaveChangesAsync();

        // إلغاء الشهادة
        Task RevokeAsync(Certificate certificate);

        // البحث والتحقق بواسطة Verification Code (للتحقق العام)
        Task<Certificate> GetByVerificationCodeAsync(string verificationCode);
    }
}
