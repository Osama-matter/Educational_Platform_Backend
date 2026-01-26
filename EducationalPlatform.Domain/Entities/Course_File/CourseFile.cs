using EducationalPlatform.Domain.Entities.Leeson;
using EducationalPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities.Course_File
{
    public  class CourseFile
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        public Guid? LessonId { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; } = default!;

        [Required]
        public CourseFileType FileType { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        [MaxLength(500)]
        public string BlobStorageUrl { get; set; } = default!;

        public int? DurationSeconds { get; set; }

        [Required]
        public Guid UploadedById { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /* =======================
           Navigation Properties
           ======================= */

        public Course.Course Course { get; set; } = default!;
        public Lesson? Lesson { get; set; }
        public User UploadedBy { get; set; } = default!;

        

    }
}
