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
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.CertificateNumber).IsUnique();
            builder.HasIndex(c => c.VerificationCode).IsUnique();
            builder.HasIndex(c => new { c.UserId, c.CourseId }).IsUnique();

            builder.Property(c => c.CertificateNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.VerificationCode)
                   .IsRequired()
                   .HasMaxLength(100);

         
        }
    }
}
