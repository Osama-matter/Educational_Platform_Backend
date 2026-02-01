using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Certificate
{
    public class AdminCertificateDto
    {
        public Guid Id { get; set; }
        public string CertificateNumber { get; set; }
        public string UserEmail { get; set; }
        public string CourseTitle { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}
