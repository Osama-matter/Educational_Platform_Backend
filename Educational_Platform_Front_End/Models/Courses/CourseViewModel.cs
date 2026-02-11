using System.Text.Json.Serialization;

namespace Educational_Platform_Front_End.Models.Courses
{
    public class CourseViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("instructorId")]
        public Guid InstructorId { get; set; }

        [JsonPropertyName("estimatedDurationHours")]
        public int? EstimatedDurationHours { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("image_URl")]
        public string Image_URl { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

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

        [JsonPropertyName("instructorName")]
        public string InstructorName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
