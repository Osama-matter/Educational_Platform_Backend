using Educational_Platform_Front_End.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var baseUrl = _configuration["ApiConfig:BaseUrl"] ?? "https://matterhub.runasp.net";
            using var client = new HttpClient();
            var payload = JsonSerializer.Serialize(new { email = Input.Email, password = Input.Password });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{baseUrl}/api/Account/login", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<LoginResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (string.IsNullOrWhiteSpace(loginResult?.Token))
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please try again.");
                return Page();
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(loginResult.Token);
            var claims = jwt.Claims.ToList();

            var roleClaims = claims
                .Where(claim => claim.Type.EndsWith("/role", StringComparison.OrdinalIgnoreCase) ||
                                claim.Type.Equals("role", StringComparison.OrdinalIgnoreCase) ||
                                claim.Type.Equals("roles", StringComparison.OrdinalIgnoreCase))
                .Select(claim => claim.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var role in roleClaims)
            {
                if (!claims.Any(claim => claim.Type == ClaimTypes.Role && claim.Value.Equals(role, StringComparison.OrdinalIgnoreCase)))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            if (!claims.Any(claim => claim.Type == ClaimTypes.NameIdentifier))
            {
                var subject = jwt.Subject ?? Input.Email;
                claims.Add(new Claim(ClaimTypes.NameIdentifier, subject));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            Response.Cookies.Append("jwt_token", loginResult.Token, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(12)
            });

            var isAdmin = claims.Any(claim =>
                claim.Type == ClaimTypes.Role &&
                string.Equals(claim.Value, "Admin", StringComparison.OrdinalIgnoreCase));

            return RedirectToPage(isAdmin ? "/Dashboard/AdminDashboard" : "/Dashboard/Index");
        }

        private sealed class LoginResponse
        {
            public string Token { get; set; }
        }
    }
}
