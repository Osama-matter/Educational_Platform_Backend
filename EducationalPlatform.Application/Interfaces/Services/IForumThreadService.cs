using EducationalPlatform.Application.DTOs.Forum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IForumThreadService
    {
        Task<ForumThreadDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ForumThreadDto>> GetAllAsync();
        Task<ForumThreadDto> CreateAsync(CreateForumThreadDto createDto, Guid userId);
        Task<ForumThreadDto> UpdateAsync(Guid id, UpdateForumThreadDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
