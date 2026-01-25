using System;

namespace Educational_Platform_Front_End.Models.Progress
{
    public class ProgressViewModel
    {
        public Guid EnrollmentId { get; set; }
        public Guid LessonId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
