using Amazon.Runtime;

namespace EducationalPlatform.Domain.Entities
{
    public class ForumPost
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public Guid ForumThreadId { get; set; }

        public Guid UserId { get; set; }

        public Guid? ParentPostId { get; set; }

        public bool IsHelpful { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ForumThread ForumThread { get; set; }

        public User User { get; set; }

        public ICollection<ForumPost> Replies { get; set; }
    }

}