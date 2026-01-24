using Educational_Platform_Front_End.Services.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Admin
{
    public abstract class AdminPageModel : PageModel
    {
        private readonly IAdminAuthService _adminAuthService;

        protected AdminPageModel(IAdminAuthService adminAuthService)
        {
            _adminAuthService = adminAuthService;
        }

        protected async Task<IActionResult> RequireAdminAsync()
        {
            var token = Request.Cookies["jwt_token"];
            if (string.IsNullOrWhiteSpace(token))
            {
                return RedirectToPage("/Account/Login");
            }

            var isAdmin = await _adminAuthService.IsAdminAsync(token);
            if (!isAdmin)
            {
                return Forbid();
            }

            return null;
        }

        protected string GetToken() => Request.Cookies["jwt_token"];
    }
}
