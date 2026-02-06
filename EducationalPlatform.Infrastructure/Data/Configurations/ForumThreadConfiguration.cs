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
    public class ForumThreadConfiguration : IEntityTypeConfiguration<ForumThread>
    {
        public void Configure(EntityTypeBuilder<ForumThread> builder)
        {
            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Posts)
                .WithOne(p => p.ForumThread)
                .HasForeignKey(p => p.ForumThreadId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
