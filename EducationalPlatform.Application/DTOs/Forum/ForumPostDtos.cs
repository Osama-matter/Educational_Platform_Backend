using System;

namespace EducationalPlatform.Application.DTOs.Forum
{
    public class ForumPostDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid ForumThreadId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? ParentPostId { get; set; }
        public bool IsHelpful { get; set; }
        public int VoteCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ForumPostDto> Replies { get; set; } = new();
    }

    public class CreateForumPostDto
    {
        public string Content { get; set; }
        public Guid ForumThreadId { get; set; }
        public Guid? ParentPostId { get; set; }
    }

    public class UpdateForumPostDto
    {
        public string Content { get; set; }
        public bool IsHelpful { get; set; }
    }
}
