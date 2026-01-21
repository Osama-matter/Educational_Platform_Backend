namespace EducationalPlatform.API.Routes
{
    public static class Routes
    {

        public  static class Courses
        {
            public const string GetAllCourses = "api/courses";
            public const string GetCourseById = "api/courses/{courseId}";
            public const string CreateCourse = "api/courses";
            public const string UpdateCourse = "api/courses/{courseId}";
            public const string DeleteCourse = "api/courses/{courseId}";
        }
    }
}
