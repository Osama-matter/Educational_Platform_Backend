using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Educational_Platform_Front_End.DTOs.Certificate;

namespace Educational_Platform_Front_End.Services.Certificate
{
    public class CertificateService : ICertificateService
    {
        private readonly HttpClient _httpClient;

        public CertificateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CertificateDto> IssueCertificateAsync(CreateCertificateDto createDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Certificates", createDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CertificateDto>();
        }

        public async Task<List<CertificateDto>> GetUserCertificatesAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Certificates/user/{userId}");
                if (!response.IsSuccessStatusCode) return new List<CertificateDto>();
                
                return await response.Content.ReadFromJsonAsync<List<CertificateDto>>() ?? new List<CertificateDto>();
            }
            catch
            {
                return new List<CertificateDto>();
            }
        }

        public async Task<VerifyCertificateDto> VerifyCertificateAsync(string verificationCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Certificates/verify/{verificationCode}");
                if (!response.IsSuccessStatusCode) return null;
                return await response.Content.ReadFromJsonAsync<VerifyCertificateDto>();
            }
            catch { return null; }
        }

        public async Task<bool> CertificateExistsAsync(Guid userId, Guid courseId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Certificates/exists/user/{userId}/course/{courseId}");
                if (!response.IsSuccessStatusCode) return false;
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch { return false; }
        }

        public string GetDownloadUrl(Guid certificateId)
        {
            // Use local backend URL directly for downloads
            return $"https://matterhub.runasp.net/api/Certificates/{certificateId}/download";
        }
    }
}
