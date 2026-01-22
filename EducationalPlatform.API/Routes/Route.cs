namespace EducationalPlatform.API.Routes
{
    public static class Routes
    {

        public static class Courses
        {
            public const string GetAllCourses = "api/courses";
            public const string GetCourseById = "api/courses/{courseId}";
            public const string CreateCourse = "api/courses";
            public const string UpdateCourse = "api/courses/{courseId}";
            public const string DeleteCourse = "api/courses/{courseId}";
        }

        public static class Lessons
        {
            public const string GetAllLessons = "api/lessons";
            public const string GetLessonById = "api/lessons/{lessonId}";
            public const string CreateLesson = "api/lessons";
            public const string UpdateLesson = "api/lessons/{lessonId}";
            public const string DeleteLesson = "api/lessons/{lessonId}";
        }

        public static class Enrollments
        {
            public const string GetAllEnrollments = "api/enrollments";
            public const string GetEnrollmentById = "api/enrollments/{enrollmentId}";
            public const string CreateEnrollment = "api/enrollments";
            public const string UpdateEnrollment = "api/enrollments/{enrollmentId}";
            public const string DeleteEnrollment = "api/enrollments/{enrollmentId}";
        }
    }
}
