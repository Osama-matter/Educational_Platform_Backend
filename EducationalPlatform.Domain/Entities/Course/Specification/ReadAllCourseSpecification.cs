using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EducationalPlatform.Domain.Entities.Course.Specification
{
    public class ReadAllCourseSpecification: SingleResultSpecification<Domain.Entities.Course.Course>  
    {
        public ReadAllCourseSpecification() 
        {
            Query.Where(course => course.Id != null );
        }
    }
}
