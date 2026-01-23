
using Microsoft.AspNetCore.Http;
using System;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class UpdateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Image_form { get; set; }
 
    }
}
