using EducationalPlatform.Application.Interfaces.External_services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services.External_services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _uploadsFolder;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            var folderPath = Path.Combine(_uploadsFolder, subfolder);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine("uploads", subfolder, fileName).Replace('\\', '/');
        }

        public Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return Task.CompletedTask;

            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            return Task.CompletedTask;
        }
    }
}
