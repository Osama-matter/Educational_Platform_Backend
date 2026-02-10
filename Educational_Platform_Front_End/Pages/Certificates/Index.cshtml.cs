using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Educational_Platform_Front_End.DTOs.Certificate;
using Educational_Platform_Front_End.Services.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_Platform_Front_End.Pages.Certificates
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICertificateService _certificateService;

        public IndexModel(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        public List<CertificateDto> Certificates { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                Certificates = await _certificateService.GetUserCertificatesAsync(userId);
            }

            return Page();
        }

        public string GetDownloadUrl(Guid certificateId)
        {
            return _certificateService.GetDownloadUrl(certificateId);
        }
    }
}
