using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class ForumSubscriptions
    {
        public Guid Id { get; set; }
        public Guid ForumThreadId { get; set; }

        public Guid UserId { get; set; }

        public ForumThread Forumthread { get; set; }

        public User User { get; set; }
    }
}
