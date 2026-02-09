using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Educational_Platform_Front_End.Services.Forum;
using EducationalPlatform.Application.DTOs.Forum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Forum
{
    public class CreateModel : PageModel
    {
        private readonly IForumService _forumService;

        public CreateModel(IForumService forumService)
        {
            _forumService = forumService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(200, MinimumLength = 5)]
            public string Title { get; set; }

            [Required]
            [MinLength(10)]
            public string Description { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var createDto = new CreateForumThreadDto
                {
                    Title = Input.Title,
                    Description = Input.Description
                };

                var result = await _forumService.CreateThreadAsync(createDto);
                return RedirectToPage("/Forum/ThreadDetails", new { id = result.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Failed to create thread: {ex.Message}");
                return Page();
            }
        }
    }
}
