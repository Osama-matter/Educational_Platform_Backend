using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Certificate
{
    public class RevokeCertificateDto
    {
        public Guid CertificateId { get; set; }
        public string Reason { get; set; }
    }
}
