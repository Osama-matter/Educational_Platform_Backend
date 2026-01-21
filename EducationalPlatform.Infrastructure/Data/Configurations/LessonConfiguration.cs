using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalPlatform.Infrastructure.Data.Configurations
{
    internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasOne(l => l.Course)
                   .WithMany(c => c.Lessons)
                   .HasForeignKey(l => l.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => new { l.CourseId, l.OrderIndex });
        }
    }
}
