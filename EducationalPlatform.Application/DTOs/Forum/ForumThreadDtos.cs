using System;

namespace EducationalPlatform.Application.DTOs.Forum
{
    public class ForumThreadDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateForumThreadDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateForumThreadDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
