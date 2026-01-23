using Educational_Platform_Front_End.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Account
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public UserDetailsViewModel UserDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["jwt_token"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login");
            }

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("https://localhost:7228/api/account/details");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    UserDetails = JsonSerializer.Deserialize<UserDetailsViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Page();
                }
                else
                {
                    // Handle error, e.g., token expired or invalid
                    return RedirectToPage("/Account/Login");
                }
            }
        }
    }
}
