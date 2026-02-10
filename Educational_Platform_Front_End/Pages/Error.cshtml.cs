using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Educational_Platform_Front_End.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public int? StatusCode { get; set; }

        public string Title { get; set; } = "Oops! Something went wrong.";

        public string Message { get; set; } = "We encountered an unexpected error while processing your request. Our team has been notified.";

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int? statusCode = null)
        {
            StatusCode = statusCode;
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            if (StatusCode == 404)
            {
                Title = "Page not found.";
                Message = "The page you are looking for doesn’t exist or may have been moved.";
            }
            else if (StatusCode == 403)
            {
                Title = "Access denied.";
                Message = "You don’t have permission to access this page.";
            }
            else if (StatusCode == 401)
            {
                Title = "Please sign in.";
                Message = "You need to log in to continue.";
            }
        }
    }

}
