using EducationalPlatform.Domain.Entities.Course_File;
using EducationalPlatform.Domain.Entities.progress;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Data.Configurations
{
    public class CourseFileConfigration : IEntityTypeConfiguration<CourseFile>
    {
        public void Configure(EntityTypeBuilder<CourseFile> builder)
        {
            builder.HasOne(cf => cf.Course)
                   .WithMany(c => c.CourseFiles)
                   .HasForeignKey(cf => cf.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cf => cf.Lesson)
                   .WithMany(l => l.CourseFiles)
                   .HasForeignKey(cf => cf.LessonId)
                   .OnDelete(DeleteBehavior.NoAction);
     

        }
    }
}
