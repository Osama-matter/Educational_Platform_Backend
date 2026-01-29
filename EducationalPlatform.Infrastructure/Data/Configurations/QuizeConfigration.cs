using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Data.Configurations
{
    public  class QuizeConfigration
    {

        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            // Table
            builder.ToTable("Quizzes");

            // Primary Key
            builder.HasKey(q => q.Id);

            // Properties
            builder.Property(q => q.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(q => q.Description)
                   .HasMaxLength(1000);

            builder.Property(q => q.DurationMinutes)
                   .IsRequired();

            builder.Property(q => q.TotalScore)
                   .IsRequired();

            builder.Property(q => q.PassingScore)
                   .IsRequired();

            builder.Property(q => q.IsPublished)
                   .IsRequired();

            builder.Property(q => q.AvailableFrom)
                   .IsRequired();

            builder.Property(q => q.AvailableTo)
                   .IsRequired();

            // Relationships
            builder.HasMany(q => q.Questions)
                   .WithOne(qs => qs.Quiz)
                   .HasForeignKey(qs => qs.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.QuizAttempts)
                   .WithOne(qa => qa.Quiz)
                   .HasForeignKey(qa => qa.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(q => q.LessonId);
        }
    }
}
