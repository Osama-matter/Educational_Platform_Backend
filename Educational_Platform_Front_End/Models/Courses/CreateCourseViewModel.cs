using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Educational_Platform_Front_End.Models.Courses
{
    public class CreateCourseViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int? EstimatedDurationHours { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 100)]
        public int NumberOfSections { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
