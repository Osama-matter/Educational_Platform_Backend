namespace EducationalPlatform.API.Routes
{
    public static class Routes
    {

        public static class Courses
        {
            public const string GetAllCourses = "";
            public const string GetCourseById = "{courseId}";
            public const string CreateCourse = "";
            public const string UpdateCourse = "{courseId}";
            public const string DeleteCourse = "{courseId}";
        }

        public static class Lessons
        {
            public const string GetAllLessons = "";
            public const string GetLessonById = "{lessonId}";
            public const string CreateLesson = "";
            public const string UpdateLesson = "{lessonId}";
            public const string DeleteLesson = "{lessonId}";
        }

        public static class Enrollments
        {
            public const string GetAllEnrollments = "";
            public const string GetEnrollmentById = "{enrollmentId}";
            public const string CreateEnrollment = "";
            public const string UpdateEnrollment = "{enrollmentId}";
            public const string DeleteEnrollment = "{enrollmentId}";
        }

        public static class Progress
        {
            public const string GetAllProgress = "";
            public const string GetProgressById = "{progressId}";
            public const string CreateProgress = "";
            public const string UpdateProgress = "{progressId}";
            public const string DeleteProgress = "{progressId}";
        }
    }
}
