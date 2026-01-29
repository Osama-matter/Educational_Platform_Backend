# EduPlatform: Enterprise Learning Management System

EduPlatform is a robust, enterprise-level Learning Management System (LMS) designed to provide a seamless and scalable educational experience. Built on a modern .NET technology stack, it offers a comprehensive suite of tools for students, instructors, and administrators to manage and engage with educational content effectively.

## 🚀 Project Overview

This platform is engineered to solve the core challenges of online education by providing a centralized, secure, and intuitive environment for learning. It addresses the need for structured course management, progress tracking, and role-based access control, making it suitable for educational institutions and corporate training programs.

### Target Users

*   **Students**: Can enroll in courses, track their progress, and access lesson materials.
*   **Instructors**: Can create and manage courses and lessons (future functionality).
*   **Admins**: Have full control over the platform, including user management, course creation, and system settings.

## 🛠️ Tech Stack & Architecture

The project is built using **ASP.NET Core** following the principles of **Clean Architecture**. This ensures a clear separation of concerns, maintainability, and scalability. The solution is API-driven, with a distinct frontend consuming the backend services.

*   **Backend**: ASP.NET Core Web API (.NET)
*   **Frontend**: ASP.NET Razor Pages
*   **Architecture**: Clean Architecture, API-based
*   **Database**: SQL Server with Entity Framework Core
*   **Authentication**: JWT-based authentication and role-based authorization

## ✨ Core Features

### Authentication & Authorization

*   **Secure User Registration & Login**: Endpoints for creating user accounts and issuing JWT tokens.
*   **Role-Based Access Control**: Differentiated access levels for Admins, Instructors, and Students, ensuring users can only perform actions appropriate for their role.

### Course Management

*   **Full CRUD Operations**: Admins can create, read, update, and delete courses through dedicated API endpoints.
*   **Public Course Catalog**: A browsable catalog of all available courses for students.
*   **Detailed Course View**: Each course has a dedicated page with a description, lesson list, and enrollment options.

### Lesson Management

*   **Structured Lessons**: Lessons are linked to specific courses, with properties for title, video URL, and duration.
*   **Admin-Only Control**: Only administrators can create, update, and delete lessons, ensuring content quality and consistency.

### Enrollment & Progress Tracking

*   **Course Enrollment**: Students can easily enroll in courses from the catalog.
*   **Lesson Completion**: The system tracks which lessons a student has completed.
*   **Progress Calculation**: Course progress is calculated and displayed as a percentage based on completed lessons.

### Admin Dashboard

*   **Centralized Management**: A secure dashboard for admins to manage all aspects of the platform.
*   **Restricted Access**: The dashboard is protected and accessible only to users with the 'Admin' role.

## 🏗️ Project Structure

The solution is organized into four distinct layers, following Clean Architecture principles:

*   **`EducationalPlatform.Domain`**: Contains the core business entities and logic, independent of any other layer.
*   **`EducationalPlatform.Application`**: Implements the business logic and use cases, orchestrating the flow of data between the domain and infrastructure.
*   **`EducationalPlatform.Infrastructure`**: Provides implementations for external concerns, such as the database context, repositories, and authentication services.
*   **`EducationalPlatform.API`**: The presentation layer, exposing the application's functionality through RESTful API endpoints. It handles HTTP requests, responses, and DTO mapping.
*   **`Educational_Platform_Front_End`**: The frontend application, built with Razor Pages, that consumes the API.

## ⚙️ Setup & Installation

### Prerequisites

*   .NET 8 SDK
*   SQL Server
*   A valid `appsettings.json` configuration

### Environment Configuration

1.  **Clone the repository**.
2.  **Configure the database connection**: Open `appsettings.json` in the `EducationalPlatform.API` project and update the `DefaultConnection` string with your SQL Server credentials.
3.  **Configure JWT settings**: In the same file, update the `Jwt` section with your own secret key and issuer details.

### Database Migration

1.  Open a terminal and navigate to the `EducationalPlatform.Infrastructure` directory.
2.  Run the following command to apply the database migrations:
    ```bash
    dotnet ef database update --startup-project ../EducationalPlatform.API
    ```

### Running the Project

1.  Run the `EducationalPlatform.API` project to start the backend server.
2.  Run the `Educational_Platform_Front_End` project to launch the web application.

## 🔮 Future Improvements

*   **Video Streaming**: Integrate a dedicated video processing and streaming service.
*   **Certificates**: Automatically generate and issue certificates upon course completion.
*   **Payment Integration**: Add support for payment gateways to allow for paid courses.
*   **Notifications**: Implement a notification system for course updates and reminders.

## 📄 License

This project is licensed under the MIT License. See the `LICENSE` file for details.



<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/d86e169b-2472-4e7c-9b40-4bb9d8cc0be1" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/aef05100-9dca-4cc8-b2e2-b9badacbec38" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/91e89938-c8d8-4203-9d18-269553c51e6d" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/2632b2f6-083a-4e83-828d-4e9d7782ad30" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/b10e4ab3-e237-44fe-a74e-12c61468dc10" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/27e1b785-e4c8-4c84-93f6-c4f47f903475" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/a5bd2986-0783-42df-9885-cf1d73039d2e" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/009298a1-cf67-4008-bf9b-05e1fb4f8519" />
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/f11008e4-7ffa-49a8-ad30-2271267587bc" />
