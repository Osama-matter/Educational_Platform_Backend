using EducationalPlatform.Application.DTOs.Forum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IForumPostService
    {
        Task<ForumPostDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ForumPostDto>> GetByThreadIdAsync(Guid threadId);
        Task<ForumPostDto> CreateAsync(CreateForumPostDto createDto, Guid userId);
        Task<ForumPostDto> UpdateAsync(Guid id, UpdateForumPostDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
