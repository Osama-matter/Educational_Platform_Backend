using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Educational_Platform_Front_End.Services.Forum;
using EducationalPlatform.Application.DTOs.Forum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Forum
{
    public class ThreadDetailsModel : PageModel
    {
        private readonly IForumService _forumService;

        public ThreadDetailsModel(IForumService forumService)
        {
            _forumService = forumService;
        }

        public ForumThreadDto Thread { get; set; }
        public List<ForumPostViewModel> Posts { get; set; } = new();
        public bool IsSubscribed { get; set; }
        public int? UserThreadVote { get; set; }

        public class ForumPostViewModel
        {
            public ForumPostDto Post { get; set; }
            public int? UserVote { get; set; }
            public List<ForumPostViewModel> Replies { get; set; } = new();
        }

        [BindProperty]
        public string ReplyContent { get; set; }
        
        [BindProperty]
        public Guid? ParentPostId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            try
            {
                Thread = await _forumService.GetThreadByIdAsync(id);
                if (Thread == null) return NotFound();

                var rootPostDtos = await _forumService.GetThreadPostsAsync(id);
                Posts = await MapPostsToViewModels(rootPostDtos);

                if (User.Identity.IsAuthenticated)
                {
                    IsSubscribed = await _forumService.IsSubscribedAsync(id);
                    UserThreadVote = await _forumService.GetMyThreadVoteAsync(id);
                }

                return Page();
            }
            catch (Exception ex)
            {
                // Handle or log error
                return NotFound();
            }
        }

        public async Task<IActionResult> OnPostVoteThreadAsync(Guid id, int value)
        {
            if (!User.Identity.IsAuthenticated) return Challenge();

            await _forumService.VoteThreadAsync(id, value);
            return RedirectToPage(new { id });
        }

        private async Task<List<ForumPostViewModel>> MapPostsToViewModels(IEnumerable<ForumPostDto> postDtos)
        {
            var viewModels = new List<ForumPostViewModel>();
            foreach (var post in postDtos)
            {
                int? userVote = null;
                if (User.Identity.IsAuthenticated)
                {
                    userVote = await _forumService.GetMyVoteAsync(post.Id);
                }
                
                var vm = new ForumPostViewModel 
                { 
                    Post = post, 
                    UserVote = userVote,
                    Replies = await MapPostsToViewModels(post.Replies)
                };
                viewModels.Add(vm);
            }
            return viewModels;
        }

        public async Task<IActionResult> OnPostVoteAsync(Guid id, Guid postId, int value)
        {
            if (!User.Identity.IsAuthenticated) return Challenge();

            await _forumService.VoteAsync(postId, value);
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostVoteCommentAsync(Guid id, Guid postId, int value)
        {
            if (!User.Identity.IsAuthenticated) return Challenge();

            await _forumService.VoteAsync(postId, value);
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostDeletePostAsync(Guid id, Guid postId)
        {
            if (!User.Identity.IsAuthenticated) return Challenge();

            var success = await _forumService.DeletePostAsync(postId);
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostEditPostAsync(Guid id, Guid postId, string content)
        {
            if (!User.Identity.IsAuthenticated) return Challenge();

            await _forumService.UpdatePostAsync(postId, new UpdateForumPostDto { Content = content });
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostPostReplyAsync(Guid id)
        {
            if (string.IsNullOrWhiteSpace(ReplyContent))
            {
                return RedirectToPage(new { id });
            }

            var createDto = new CreateForumPostDto
            {
                ForumThreadId = id,
                Content = ReplyContent,
                ParentPostId = ParentPostId
            };

            await _forumService.CreatePostAsync(createDto);
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostSubscribeAsync(Guid id)
        {
            await _forumService.SubscribeAsync(new CreateForumSubscriptionDto { ForumThreadId = id });
            return RedirectToPage(new { id });
        }

        public async Task<IActionResult> OnPostUnsubscribeAsync(Guid id)
        {
            await _forumService.UnsubscribeAsync(id);
            return RedirectToPage(new { id });
        }
    }
}
