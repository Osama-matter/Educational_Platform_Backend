# Educational Platform Backend Documentation

## Overview
This is a modern, clean-architecture-based backend for an Educational Platform. It is built using **ASP.NET Core 8**, following **Domain-Driven Design (DDD)** principles and a layered architecture.

## Architecture
The project is divided into four main layers:

1.  **EducationalPlatform.Domain**: Contains core entities, enums, and domain logic. It has no dependencies on other layers.
2.  **EducationalPlatform.Application**: Defines interfaces, DTOs (Data Transfer Objects), and application logic. It depends only on the Domain layer.
3.  **EducationalPlatform.Infrastructure**: Implements the interfaces defined in the Application layer, including data persistence (EF Core), security (JWT), and external services (Email, File Storage).
4.  **EducationalPlatform.API**: The entry point of the application, containing controllers, middleware, and API configurations.

## Core Features

### 1. User Management & Authentication
-   **Identity**: Uses ASP.NET Core Identity for user management.
-   **JWT Authentication**: Secure token-based authentication with role-based authorization.
-   **Password Hashing**: Custom password hashing implementation.

### 2. Course Management
-   **Course Lifecycle**: Create, update, delete, and retrieve courses.
-   **Eager Loading**: Courses include related lessons, reviews, and files when retrieved.
-   **Image Support**: Courses have associated images stored via a file storage service.

### 3. Learning Progress
-   **Enrollment**: Users can enroll in courses.
-   **Lesson Progress**: Tracks user progress through individual lessons.
-   **Quizzes**: Supports quizzes with multiple-choice questions and options.
-   **Quiz Attempts**: Tracks user attempts and scores for each quiz.

### 4. Reviews & Interaction
-   **Reviews**: Users can leave ratings and comments on courses.
-   **Instructor Replies**: Instructors can reply to user reviews.

### 5. Certification
-   **Automatic Generation**: Generates PDF certificates upon course completion.
-   **Verification**: Provides a system to verify certificates via unique codes.

### 6. Communication
-   **Email Service**: Sends welcome emails, enrollment confirmations, certificates, and weekly digests using SMTP (Gmail).
-   **Weekly Digest**: A background hosted service that sends periodic updates to users.

## Technical Stack
-   **Framework**: .NET 8
-   **Database**: SQL Server (via Entity Framework Core)
-   **Authentication**: JWT Bearer
-   **Email**: MailKit
-   **File Storage**: Local file system (expandable to Azure Blob Storage)
-   **Documentation**: Swagger/OpenAPI

## Project Structure
```text
Educational_Platform_Backend/
├── EducationalPlatform.API/          # Controllers, Routes, Middleware
├── EducationalPlatform.Application/   # Interfaces, DTOs
├── EducationalPlatform.Domain/        # Entities, Enums
├── EducationalPlatform.Infrastructure/ # Persistence, Repositories, Services
└── Educational_Platform_Backend.sln   # Solution File
```

## Getting Started
1.  **Database**: Update the connection string in `appsettings.json`.
2.  **Migrations**: Run `Update-Database` to create the schema.
3.  **Email Configuration**: Set up Google App Password in `appsettings.json` for the email service.
4.  **Run**: Launch the API project to view the Swagger documentation.
