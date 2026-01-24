using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class LessonManagementModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Admin/Lessons/Index");
        }
    }
}
