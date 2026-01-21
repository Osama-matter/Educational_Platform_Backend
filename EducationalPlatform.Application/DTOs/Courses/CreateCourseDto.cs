using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Domain.Entities.Course;

namespace EducationalPlatform.Application.DTOs.Courses
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorId { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }

        public Course ToEntity()
        {
            return new Course(Title, Description, InstructorId, EstimatedDurationHours, IsActive);
        }
    }
}
