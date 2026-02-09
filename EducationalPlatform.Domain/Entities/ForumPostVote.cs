using System;

namespace EducationalPlatform.Domain.Entities
{
    public class ForumPostVote
    {
        public Guid Id { get; set; }
        public Guid? ForumPostId { get; set; }
        public Guid? ForumThreadId { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; } // 1 for upvote, -1 for downvote
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ForumPost? ForumPost { get; set; }
        public ForumThread? ForumThread { get; set; }
        public User User { get; set; }
    }
}
