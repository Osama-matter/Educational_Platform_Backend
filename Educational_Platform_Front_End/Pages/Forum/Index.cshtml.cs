using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_Platform_Front_End.Services.Forum;
using EducationalPlatform.Application.DTOs.Forum;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Forum
{
    public class IndexModel : PageModel
    {
        private readonly IForumService _forumService;

        public IndexModel(IForumService forumService)
        {
            _forumService = forumService;
        }

        public IEnumerable<ForumThreadDto> Threads { get; set; } = new List<ForumThreadDto>();
        public IEnumerable<ForumSubscriptionDto> SubscribedThreads { get; set; } = new List<ForumSubscriptionDto>();

        public async Task OnGetAsync()
        {
            try
            {
                Console.WriteLine("[DEBUG] Index Page OnGetAsync starting...");
                Threads = await _forumService.GetAllThreadsAsync();
                
                if (Threads != null)
                {
                    Console.WriteLine($"[DEBUG] Index Page received {Threads.Count()} threads");
                }
                else
                {
                    Console.WriteLine("[DEBUG] Index Page received NULL threads collection");
                    Threads = new List<ForumThreadDto>();
                }
                
                if (User.Identity.IsAuthenticated)
                {
                    SubscribedThreads = await _forumService.GetMySubscriptionsAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Critical Error in Forum Index Page: {ex.Message}");
                Console.WriteLine($"[DEBUG] StackTrace: {ex.StackTrace}");
                Threads = new List<ForumThreadDto>();
            }
        }
    }
}
