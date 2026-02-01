using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class Certificate
    {
        // Primary Key
        public Guid Id { get; private set; }

        // Relations
        public Guid UserId { get; private set; }
        public Guid CourseId { get; private set; }

        // Certificate Info
        public string CertificateNumber { get; private set; }
        public string VerificationCode { get; private set; }

        public DateTime IssuedAt { get; private set; }
        
        public bool IsRevoked { get; private set; }

        // Navigation Properties (optional but recommended)
        public User User { get; private set; }
        public Course.Course Course { get; private set; }

        // EF Core constructor
        private Certificate() { }

        // Factory constructor
        public Certificate(
            Guid userId,
            Guid courseId,
            string certificateNumber,
            string verificationCode
            )
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CourseId = courseId;

            CertificateNumber = certificateNumber;
            VerificationCode = verificationCode;

                        IssuedAt = DateTime.UtcNow;

            IsRevoked = false;
        }

        // Business Methods
        public void Revoke()
        {
            IsRevoked = true;
        }
    }
}
