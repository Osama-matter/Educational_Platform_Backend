# EduPlatform: Enterprise Learning Management System

EduPlatform is a robust, enterprise-level Learning Management System (LMS) designed to provide a seamless and scalable educational experience. Built on a modern .NET technology stack, it offers a comprehensive suite of tools for students, instructors, and administrators to manage and engage with educational content effectively.

## 🚀 Project Overview

This platform is engineered to solve the core challenges of online education by providing a centralized, secure, and intuitive environment for learning. It addresses the need for structured course management, progress tracking, interactive assessments, and role-based access control, making it suitable for educational institutions and corporate training programs.

### Target Users

*   **Students**: Can enroll in courses, track their progress, access lesson materials, take quizzes, and view their assessment results in real-time.
*   **Instructors**: Can create and manage courses and lessons, design comprehensive quizzes with multiple question types, and monitor student performance.
*   **Admins**: Have full control over the platform, including user management, course creation, quiz management, question bank administration, and system settings.

## 🛠️ Tech Stack & Architecture

The project is built using **ASP.NET Core** following the principles of **Clean Architecture**. This ensures a clear separation of concerns, maintainability, and scalability. The solution is API-driven, with a distinct frontend consuming the backend services.

*   **Backend**: ASP.NET Core Web API (.NET 8)
*   **Frontend**: ASP.NET Razor Pages
*   **Architecture**: Clean Architecture, API-based
*   **Database**: SQL Server with Entity Framework Core
*   **Authentication**: JWT-based authentication and role-based authorization
*   **ORM**: Entity Framework Core with Code-First Migrations

## ✨ Core Features

### Authentication & Authorization

*   **Secure User Registration & Login**: Endpoints for creating user accounts and issuing JWT tokens.
*   **Role-Based Access Control**: Differentiated access levels for Admins, Instructors, and Students, ensuring users can only perform actions appropriate for their role.
*   **Token-Based Security**: JWT tokens with configurable expiration and refresh mechanisms.

### Course Management

*   **Full CRUD Operations**: Admins and Instructors can create, read, update, and delete courses through dedicated API endpoints.
*   **Public Course Catalog**: A browsable catalog of all available courses for students.
*   **Detailed Course View**: Each course has a dedicated page with description, lesson list, associated quizzes, and enrollment options.
*   **Course Metadata**: Rich course information including difficulty level, duration, and prerequisites.

### Lesson Management

*   **Structured Lessons**: Lessons are linked to specific courses, with properties for title, content, video URL, and duration.
*   **Admin & Instructor Control**: Content creators can manage lessons with version control and publishing workflows.
*   **Multimedia Support**: Support for video content, downloadable resources, and rich text materials.

### Enrollment & Progress Tracking

*   **Course Enrollment**: Students can easily enroll in courses from the catalog.
*   **Lesson Completion**: The system tracks which lessons a student has completed with timestamps.
*   **Progress Calculation**: Course progress is calculated and displayed as a percentage based on completed lessons and quiz attempts.
*   **Dashboard Analytics**: Visual representation of learning progress and achievements.

### Admin Dashboard

*   **Centralized Management**: A secure dashboard for admins to manage all aspects of the platform.
*   **Restricted Access**: The dashboard is protected and accessible only to users with the 'Admin' role.
*   **Real-Time Statistics**: Overview of active users, course enrollments, and quiz completion rates.

### 🎯 Quiz System (Core Feature)

The Quiz System is a comprehensive assessment module designed to evaluate student knowledge and provide immediate feedback.

#### Quiz Management

*   **Quiz Creation & Configuration**:
    - Link quizzes to specific courses
    - Set quiz name, description, and instructions
    - Configure total grade and passing score threshold
    - Define start and end dates for quiz availability
    - Set question count and time limits
    - Toggle quiz availability (published/draft status)

*   **Full CRUD Operations**: 
    - `POST /api/Quizzes` - Create new quiz
    - `GET /api/Quizzes` - Retrieve all quizzes
    - `GET /api/Quizzes/{quizId}` - Get specific quiz details
    - `PUT /api/Quizzes/{quizId}` - Update quiz configuration
    - `DELETE /api/Quizzes/{quizId}` - Remove quiz

#### Question Management

*   **Multiple Question Types**:
    - **Multiple Choice Questions (MCQ)**: Single correct answer from multiple options
    - **True/False Questions**: Binary choice questions
    - **Short Answer**: Text-based responses (future enhancement)

*   **Question Properties**:
    - Question content (text, formatted text support)
    - Question type selection
    - Individual question scoring/grading
    - Correct answer marking
    - Question ordering and numbering

*   **API Endpoints**:
    - `POST /api/Questions` - Add new question to quiz
    - `GET /api/Questions` - List all questions
    - `GET /api/Questions/{questionId}` - Get question details
    - `PUT /api/Questions/{questionId}` - Update question
    - `DELETE /api/Questions/{questionId}` - Remove question

#### Answer Options Management

*   **Option Configuration**:
    - Multiple answer choices per question
    - Mark correct answer(s) with `isTrue` flag
    - Support for text-based options
    - Option ordering and display

*   **API Endpoints**:
    - `POST /api/QuestionOptions` - Create answer option
    - `GET /api/QuestionOptions` - List all options
    - `GET /api/QuestionOptions/{optionId}` - Get specific option
    - `PUT /api/QuestionOptions/{optionId}` - Update option
    - `DELETE /api/QuestionOptions/{optionId}` - Remove option

#### Quiz Attempt & Submission

*   **Student Quiz Flow**:
    1. Student views available quizzes for enrolled courses
    2. System validates quiz availability (date range check)
    3. Quiz loads with all questions and options
    4. Student selects answers for each question
    5. System creates QuizAttempt record to track the session
    6. Student submits completed quiz
    7. Automatic grading compares student answers with correct answers
    8. Immediate result display with score and feedback

*   **Attempt Tracking**:
    - Each attempt is uniquely identified
    - Records user ID, quiz ID, and timestamp
    - Stores all student answers for review
    - Tracks completion status
    - Calculates and stores final score

*   **API Endpoints**:
    - `POST /api/QuizAttempts` - Start new quiz attempt
    - `GET /api/QuizAttempts` - List user's attempts
    - `GET /api/QuizAttempts/{attemptId}` - Get attempt details
    - `PUT /api/QuizAttempts/{attemptId}` - Update attempt answers
    - `POST /api/QuizAttempts/{attemptId}/submit` - Submit quiz for grading

#### Grading & Results

*   **Automatic Grading System**:
    - Compares student responses with correct answers stored in QuestionOptions
    - Calculates individual question scores
    - Aggregates total score based on question weights
    - Determines pass/fail status based on threshold
    - Generates detailed result report

*   **Result Display**:
    - Overall score and percentage
    - Pass/fail status
    - Question-by-question breakdown
    - Correct vs. student answers comparison
    - Time taken to complete quiz

*   **Admin Review**:
    - View all student attempts
    - Analyze quiz performance statistics
    - Identify difficult questions
    - Export results for reporting

#### Database Schema

The Quiz System is built on four interconnected tables:

**1. Quizzes Table**
```
- quiz_id (PK, GUID)
- course_id (FK → Courses)
- name (string)
- description (string)
- question_number (int)
- grade (float)
- start_date (datetime)
- end_date (datetime)
- is_available (bool)
```

**2. Questions Table**
```
- question_id (PK, GUID)
- quiz_id (FK → Quizzes)
- content (string)
- type (string: MCQ, TrueFalse, ShortAnswer)
- grade (float)
- is_true (bool)
```

**3. QuestionOptions Table**
```
- option_id (PK, GUID)
- question_id (FK → Questions)
- answer (string)
- is_true (bool) - marks correct answer
```

**4. QuizAttempts Table**
```
- attempt_id (PK, GUID)
- quiz_id (FK → Quizzes)
- user_id (FK → Users)
- question_id (FK → Questions)
- content (string) - student's answer
- submitted_at (datetime)
- score (float)
- is_completed (bool)
```

#### Key Relationships

- **One Course → Many Quizzes**: Each course can have multiple quizzes
- **One Quiz → Many Questions**: Each quiz contains multiple questions
- **One Question → Many Options**: MCQ questions have multiple answer choices
- **One Quiz → Many Attempts**: Students can take quizzes multiple times (configurable)
- **One User → Many Attempts**: Track all quiz attempts per student

## 🏗️ Project Structure

The solution is organized into five distinct layers, following Clean Architecture principles:

*   **`EducationalPlatform.Domain`**: Contains the core business entities and logic, independent of any other layer. Includes entities for Courses, Lessons, Quizzes, Questions, QuestionOptions, and QuizAttempts.
*   **`EducationalPlatform.Application`**: Implements the business logic and use cases, orchestrating the flow of data between the domain and infrastructure. Contains DTOs, services interfaces, validation rules, and quiz grading logic.
*   **`EducationalPlatform.Infrastructure`**: Provides implementations for external concerns, such as the database context, repositories, and authentication services. Handles EF Core mappings, migrations, and external integrations.
*   **`EducationalPlatform.API`**: The presentation layer, exposing the application's functionality through RESTful API endpoints. Handles HTTP requests, responses, DTO mapping, and API documentation (Swagger).
*   **`Educational_Platform_Front_End`**: The frontend application, built with Razor Pages, that consumes the API. Includes admin quiz management UI, student quiz-taking interface, result display, and user authentication pages.

## ⚙️ Setup & Installation

### Prerequisites

*   .NET 8 SDK or later
*   SQL Server 2019 or later (or SQL Server Express)
*   Visual Studio 2022 or VS Code
*   A valid `appsettings.json` configuration

### Environment Configuration

1.  **Clone the repository**:
    ```bash
    git clone <repository-url>
    cd EduPlatform
    ```

2.  **Configure the database connection**: 
    - Open `appsettings.json` in the `EducationalPlatform.API` project
    - Update the `DefaultConnection` string with your SQL Server credentials:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=EduPlatformDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

3.  **Configure JWT settings**: 
    - In the same file, update the `Jwt` section with your own secret key and issuer details:
    ```json
    "Jwt": {
      "Key": "your-super-secret-key-here-minimum-32-characters",
      "Issuer": "EduPlatform",
      "Audience": "EduPlatformUsers",
      "ExpiryMinutes": 60
    }
    ```

### Database Migration

1.  Open a terminal and navigate to the `EducationalPlatform.Infrastructure` directory:
    ```bash
    cd EducationalPlatform.Infrastructure
    ```

2.  Run the following command to apply the database migrations:
    ```bash
    dotnet ef database update --startup-project ../EducationalPlatform.API
    ```

3.  Verify the database has been created with all tables including the Quiz System tables (Quizzes, Questions, QuestionOptions, QuizAttempts).

### Running the Project

1.  **Start the Backend API**:
    ```bash
    cd EducationalPlatform.API
    dotnet run
    ```
    The API will be available at `https://localhost:7001` (or the port specified in launchSettings.json)

2.  **Start the Frontend**:
    ```bash
    cd Educational_Platform_Front_End
    dotnet run
    ```
    The web application will be available at `https://localhost:5001`

3.  **Access Swagger Documentation**: Navigate to `https://localhost:7001/swagger` to explore and test API endpoints.

### Initial Setup

1.  Register a new user account
2.  Assign Admin role to your user (manually in database or through seed data)
3.  Login with Admin credentials
4.  Create your first course
5.  Add lessons to the course
6.  Create a quiz and add questions
7.  Test the student experience by enrolling and taking the quiz

## 📊 API Documentation

### Quiz System Endpoints

#### Quizzes
- `GET /api/Quizzes` - Get all quizzes
- `GET /api/Quizzes/{id}` - Get quiz by ID
- `POST /api/Quizzes` - Create new quiz (Admin/Instructor)
- `PUT /api/Quizzes/{id}` - Update quiz (Admin/Instructor)
- `DELETE /api/Quizzes/{id}` - Delete quiz (Admin)

#### Questions
- `GET /api/Questions` - Get all questions
- `GET /api/Questions/{id}` - Get question by ID
- `POST /api/Questions` - Create question (Admin/Instructor)
- `PUT /api/Questions/{id}` - Update question (Admin/Instructor)
- `DELETE /api/Questions/{id}` - Delete question (Admin)

#### Question Options
- `GET /api/QuestionOptions` - Get all options
- `GET /api/QuestionOptions/{id}` - Get option by ID
- `POST /api/QuestionOptions` - Create option (Admin/Instructor)
- `PUT /api/QuestionOptions/{id}` - Update option (Admin/Instructor)
- `DELETE /api/QuestionOptions/{id}` - Delete option (Admin)

#### Quiz Attempts
- `GET /api/QuizAttempts` - Get user's attempts (Student)
- `GET /api/QuizAttempts/{id}` - Get attempt details
- `POST /api/QuizAttempts` - Start quiz attempt (Student)
- `POST /api/QuizAttempts/{id}/submit` - Submit quiz (Student)

## 🔮 Future Improvements

### Short-term Enhancements
*   **Quiz Analytics Dashboard**: Comprehensive reporting on quiz performance, question difficulty analysis, and student progress tracking
*   **Question Bank**: Reusable question library with tagging and categorization
*   **Randomized Quizzes**: Random question selection from question pools
*   **Timed Quizzes**: Built-in countdown timer with auto-submission
*   **Multiple Attempts**: Configurable retry limits with best/average/last attempt grading options

### Medium-term Enhancements
*   **Advanced Question Types**: 
    - Fill-in-the-blank
    - Matching questions
    - Ordering/sequencing
    - Essay questions with manual grading workflow
*   **Quiz Templates**: Pre-built quiz templates for common assessment scenarios
*   **Partial Credit**: Award points for partially correct answers
*   **Question Feedback**: Detailed explanations for correct/incorrect answers
*   **Quiz Scheduling**: Automated quiz publishing and archiving

### Long-term Enhancements
*   **Video Streaming**: Integrate dedicated video processing and streaming service (Azure Media Services)
*   **Certificates**: Automatically generate and issue certificates upon course completion with quiz score requirements
*   **Payment Integration**: Add support for payment gateways (Stripe, PayPal) to allow for paid courses
*   **Notifications**: Real-time notification system for quiz availability, deadline reminders, and results
*   **Mobile App**: Native iOS and Android applications for on-the-go learning
*   **Gamification**: Badges, leaderboards, and achievement system
*   **AI-Powered Features**: 
    - Automated question generation from course content
    - Intelligent difficulty adjustment
    - Personalized quiz recommendations
*   **Proctoring Integration**: Online exam monitoring and anti-cheating measures
*   **Accessibility**: WCAG 2.1 AA compliance for inclusive learning

## 🧪 Testing

### Unit Tests
- Domain logic validation
- Quiz grading algorithm verification
- Answer comparison logic

### Integration Tests
- API endpoint testing
- Database operations
- Authentication flows

### End-to-End Tests
- Complete quiz workflow
- User registration to quiz completion
- Multi-role scenarios

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## 📧 Contact

For questions, suggestions, or support, please open an issue on GitHub or contact the development team.

---

**Built with ❤️ using ASP.NET Core and Clean Architecture principles**


<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/d86e169b-2472-4e7c-9b40-4bb9d8cc0be1" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/aef05100-9dca-4cc8-b2e2-b9badacbec38" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/91e89938-c8d8-4203-9d18-269553c51e6d" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/2632b2f6-083a-4e83-828d-4e9d7782ad30" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/b10e4ab3-e237-44fe-a74e-12c61468dc10" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/27e1b785-e4c8-4c84-93f6-c4f47f903475" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/a5bd2986-0783-42df-9885-cf1d73039d2e" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/009298a1-cf67-4008-bf9b-05e1fb4f8519" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/f11008e4-7ffa-49a8-ad30-2271267587bc" />
<img width="553" height="1075" alt="quiz_system_uml" src="https://github.com/user-attachments/assets/30c8e0ff-905f-43e1-88a6-a84a755d4a4b" />

