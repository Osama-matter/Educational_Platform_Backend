using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Certificate
{
    public class CertificateDto
    {
        public Guid Id { get; set; }
        public string CertificateNumber { get; set; }
        public string CourseTitle { get; set; }
        public DateTime IssuedAt { get; set; }
        public bool IsRevoked { get; set; }
        public string DownloadUrl { get; set; }
    }
}
