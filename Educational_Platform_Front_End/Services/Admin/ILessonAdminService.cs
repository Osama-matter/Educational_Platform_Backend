using Educational_Platform_Front_End.Models.Lessons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Educational_Platform_Front_End.Services.Admin
{
    public interface ILessonAdminService
    {
        Task<IReadOnlyList<LessonViewModel>> GetLessonsAsync(string token);
        Task<LessonViewModel> GetLessonByIdAsync(string token, Guid lessonId);
        Task CreateLessonAsync(string token, CreateLessonViewModel lesson);
        Task UpdateLessonAsync(string token, Guid lessonId, UpdateLessonViewModel lesson);
        Task DeleteLessonAsync(string token, Guid lessonId);
    }
}
