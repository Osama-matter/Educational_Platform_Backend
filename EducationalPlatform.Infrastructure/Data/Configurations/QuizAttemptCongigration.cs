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
    public  class QuizAttemptCongigration 
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            // Table
            builder.ToTable("QuizAttempts");

            // Primary Key
            builder.HasKey(qa => qa.Id);

            // Properties
            builder.Property(qa => qa.UserId)
                   .IsRequired();

            builder.Property(qa => qa.StartedAt)
                   .IsRequired();

            builder.Property(qa => qa.SubmittedAt)
                   .IsRequired(false);

            builder.Property(qa => qa.TotalScore)
                   .IsRequired();

            builder.Property(qa => qa.Status)
                   .HasConversion<int>()
                   .IsRequired();

            // Relationships
            builder.HasOne(qa => qa.Quiz)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(qa => qa.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(qa => new { qa.QuizId, qa.UserId });
        }
    }

}

