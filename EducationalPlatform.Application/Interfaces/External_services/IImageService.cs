using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.External_services
{
    public interface IImageService
    {
        Task<string> SaveCourseImageAsync(IFormFile imageFile);


        public Task<string> UpdateCourseImageAsync(string existingFileName, IFormFile newImageFile);

        public Task<bool> DeleteCourseImageAsync(string fileName); 
    }
}
