using EducationalPlatform.Domain.Enums;
using System;

namespace EducationalPlatform.Application.DTOs.CourseFile
{
    public class CourseFileDto
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid? LessonId { get; set; }
        public string FileName { get; set; }
        public CourseFileType FileType { get; set; }
        public long FileSize { get; set; }
        public string BlobStorageUrl { get; set; }
        public int? DurationSeconds { get; set; }
        public Guid UploadedById { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
