using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public   class BaseEntity
    {
        public  DateTimeOffset CreatedAt { get; set; }

        public  DateTimeOffset UpdatedAt { get; set; }

    }
}
