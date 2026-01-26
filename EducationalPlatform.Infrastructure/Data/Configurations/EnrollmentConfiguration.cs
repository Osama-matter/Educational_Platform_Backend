using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EducationalPlatform.Domain.Entities;
using EducationalPlatform.Domain.Entities.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Infrastructure.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();

            builder.HasOne(e => e.Student)
                   .WithMany(u => u.Enrollments)
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Course)
                   .WithMany(c => c.Enrollments)
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
