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

        public static class Quizzes
        {
            public const string GetAllQuizzes = "";
            public const string GetQuizById = "{quizId}";
            public const string CreateQuiz = "";
            public const string UpdateQuiz = "{quizId}";
            public const string DeleteQuiz = "{quizId}";
        }

        public static class Questions
        {
            public const string GetAllQuestions = "";
            public const string GetQuestionById = "{questionId}";
            public const string CreateQuestion = "";
            public const string UpdateQuestion = "{questionId}";
            public const string DeleteQuestion = "{questionId}";
            public const string GetQuestionsByQuizId = "quiz/{quizId}";
        }

        public static class QuizAttempts
        {
            public const string GetAllQuizAttempts = "";
            public const string GetQuizAttemptById = "{quizAttemptId}";
            public const string CreateQuizAttempt = "";
            public const string UpdateQuizAttempt = "{quizAttemptId}";
            public const string DeleteQuizAttempt = "{quizAttemptId}";
            public const string SubmitAnswers = "{quizAttemptId}/submit";
        }

        public static class QuestionOptions
        {
            public const string GetAllQuestionOptions = "";
            public const string GetQuestionOptionById = "{questionOptionId}";
            public const string CreateQuestionOption = "";
            public const string UpdateQuestionOption = "{questionOptionId}";
            public const string DeleteQuestionOption = "{questionOptionId}";
            public const string GetQuestionOptionsByQuestionId = "question/{questionId}";
        }

        public static class Certificates
        {
            public const string IssueCertificate = "";
            public const string GetUserCertificates = "user/{userId}";
            public const string RevokeCertificate = "{certificateId}/revoke";
            public const string VerifyCertificate = "verify/{verificationCode}";
            public const string GetCertificateDetails = "{certificateId}";
            public const string CertificateExists = "exists/user/{userId}/course/{courseId}";
        }
    }
}

