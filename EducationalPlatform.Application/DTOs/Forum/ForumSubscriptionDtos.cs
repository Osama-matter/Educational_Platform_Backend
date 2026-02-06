using System;

namespace EducationalPlatform.Application.DTOs.Forum
{
    public class ForumSubscriptionDto
    {
        public Guid Id { get; set; }
        public Guid ForumThreadId { get; set; }
        public Guid UserId { get; set; }
        public string ForumThreadTitle { get; set; }
    }

    public class CreateForumSubscriptionDto
    {
        public Guid ForumThreadId { get; set; }
    }
}
