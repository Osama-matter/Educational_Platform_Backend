using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Educational_Platform_Front_End.Services.Enrollments
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnrollmentService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EnrollmentResult> EnrollAsync(Guid courseId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    userId = _httpContextAccessor.HttpContext?.User?.FindFirst("nameid")?.Value;
                }

                if (string.IsNullOrEmpty(userId))
                {
                    return new EnrollmentResult { Success = false, Error = "User is not authenticated or ID not found." };
                }

                // Call with empty body since API expects it
                var response = await _httpClient.PostAsJsonAsync($"/api/enrollments/{userId}/{courseId}", new { });
                
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<EnrollmentDto>();
                    return new EnrollmentResult 
                    { 
                        Success = true, 
                        PaymentUrl = result?.PaymentUrl 
                    };
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new EnrollmentResult { Success = false, Error = $"Enrollment failed ({response.StatusCode}): {errorContent}" };
            }
            catch (Exception ex)
            {
                return new EnrollmentResult { Success = false, Error = ex.Message };
            }
        }

        private class EnrollmentDto
        {
            public string? PaymentUrl { get; set; }
        }
    }
}
