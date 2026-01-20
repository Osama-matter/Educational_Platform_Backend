using EducationalPlatform.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EducationalPlatform.Infrastructure.Data
{
    public class ApplicationDbContext
     : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonProgress> LessonProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Role).HasConversion<string>(); // مؤقت
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasOne(c => c.Instructor)
                    .WithMany(u => u.CoursesCreated)
                    .HasForeignKey(c => c.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(c => c.Title);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.Enrollments)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(l => l.Course)
                    .WithMany(c => c.Lessons)
                    .HasForeignKey(l => l.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(l => new { l.CourseId, l.OrderIndex });
            });

            modelBuilder.Entity<LessonProgress>(entity =>
            {
                entity.HasIndex(lp => new { lp.EnrollmentId, lp.LessonId }).IsUnique();

                entity.HasOne(lp => lp.Enrollment)
                    .WithMany(e => e.LessonProgresses)
                    .HasForeignKey(lp => lp.EnrollmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(lp => lp.Lesson)
                    .WithMany(l => l.LessonProgresses)
                    .HasForeignKey(lp => lp.LessonId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

}


