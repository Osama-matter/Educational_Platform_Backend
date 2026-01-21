using EducationalPlatform.Domain.Entities.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces
{
    public interface ICourseService<TRequest, TResponse> : IGenericServices<TRequest, TResponse>
    {
        //Task CreateAsync(Course oData);
    }
}
