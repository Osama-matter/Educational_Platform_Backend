using System.Text.Json.Serialization;

namespace Educational_Platform_Front_End.Models.Courses
{
    public class CourseViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid InstructorId { get; set; }
        public int? EstimatedDurationHours { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Image_URl { get; set; }

        public string ImageUrl
        {
            get => Image_URl;
            set => Image_URl = value;
        }

        public int? DurationHours
        {
            get => EstimatedDurationHours;
            set => EstimatedDurationHours = value;
        }

        public string InstructorName { get; set; }
        public string Type { get; set; }
    }
}
