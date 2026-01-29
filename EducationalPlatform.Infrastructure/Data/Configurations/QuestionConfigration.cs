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
    public  class QuestionConfigration :IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            // Table
            builder.ToTable("Questions");

            // Primary Key
            builder.HasKey(q => q.Id);

            // Properties
            builder.Property(q => q.Content)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(q => q.Score)
                   .IsRequired();

            builder.Property(q => q.QuestionType)
                   .HasConversion<int>()   // store enum as int
                   .IsRequired();

            // Relationships
            builder.HasOne(q => q.Quiz)
                   .WithMany(qz => qz.Questions)
                   .HasForeignKey(q => q.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
