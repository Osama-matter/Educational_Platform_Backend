using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Certificate
{
    public class CreateCertificateDto
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
            }
}
