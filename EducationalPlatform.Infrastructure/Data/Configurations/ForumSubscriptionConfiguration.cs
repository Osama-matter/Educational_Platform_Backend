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
    public class ForumSubscriptionConfiguration : IEntityTypeConfiguration<ForumSubscriptions>
    {
        public void Configure(EntityTypeBuilder<ForumSubscriptions> builder)
        {
            builder.HasKey(fs => new { fs.UserId, fs.ForumThreadId });

            builder.HasOne(fs => fs.User)
                .WithMany()
                .HasForeignKey(fs => fs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fs => fs.Forumthread)
                .WithMany()
                .HasForeignKey(fs => fs.ForumThreadId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
