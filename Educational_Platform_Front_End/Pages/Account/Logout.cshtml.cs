using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // The actual cookie removal will be handled by client-side script
            // This page just facilitates the redirect after logout
            return Page();
        }
    }
}
