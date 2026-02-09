using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Entities
{
    public class ForumThread
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int VoteCount { get; set; }

        // Navigation Properties
        public User User { get; set; }

        public ICollection<ForumPost> Posts { get; set; }

        public ICollection<ForumPostVote> Votes { get; set; }
    }

}
