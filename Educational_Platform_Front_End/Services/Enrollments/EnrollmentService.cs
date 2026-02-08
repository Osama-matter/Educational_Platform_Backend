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
                    // Fallback to nameid if NameIdentifier is not found (common in some JWT setups)
                    userId = _httpContextAccessor.HttpContext?.User?.FindFirst("nameid")?.Value;
                }

                if (string.IsNullOrEmpty(userId))
                {
                    return new EnrollmentResult { Success = false, Error = "User is not authenticated or ID not found." };
                }

                // POST /api/enrollments/{studentId}/{courseId}
                var response = await _httpClient.PostAsync($"/api/enrollments/{userId}/{courseId}", null);
                
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Enrollment API Response: {content}");

                if (response.IsSuccessStatusCode)
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<EnrollmentResponse>(content);
                    return new EnrollmentResult 
                    { 
                        Success = true, 
                        PaymentUrl = result?.PaymentUrl 
                    };
                }

                return new EnrollmentResult { Success = false, Error = $"Enrollment failed ({response.StatusCode}): {content}" };
            }
            catch (Exception ex)
            {
                return new EnrollmentResult { Success = false, Error = ex.Message };
            }
        }

        private class EnrollmentResponse
        {
            public string? PaymentUrl { get; set; }
        }
    }
}
