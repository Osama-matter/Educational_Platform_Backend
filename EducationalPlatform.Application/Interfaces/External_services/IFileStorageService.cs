using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.External_services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string subfolder);
        Task DeleteFileAsync(string filePath);
    }
}
