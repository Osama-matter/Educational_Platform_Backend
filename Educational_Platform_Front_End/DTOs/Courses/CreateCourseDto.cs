
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorId { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }

        public IFormFile imageFile { get; set; }
  
    }
}
