using EducationalPlatform.Application.DTOs.Forum;
using EducationalPlatform.Application.Interfaces.Repositories;
using EducationalPlatform.Application.Interfaces.Services;
using EducationalPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Services
{
    public class ForumThreadService : IForumThreadService
    {
        private readonly IForumThreadRepository _repository;
        private readonly IUserRepository _userRepository;

        public ForumThreadService(IForumThreadRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;

        }

        public async Task<ForumThreadDto?> GetByIdAsync(Guid id)
        {
            var thread = await _repository.GetByIdAsync(id);
            if (thread == null) return null;

            return MapToDto(thread);
        }

        public async Task<IEnumerable<ForumThreadDto>> GetAllAsync()
        {
            var threads = await _repository.GetAllAsync();
            return threads.Select(MapToDto);
        }

        public async Task<ForumThreadDto> CreateAsync(CreateForumThreadDto createDto, Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null) throw new Exception("User not found");
            var thread = new ForumThread
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                Description = createDto.Description,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(thread);
            
            var created = await _repository.GetByIdAsync(thread.Id);
            return MapToDto(created!);
        }

        public async Task<ForumThreadDto> UpdateAsync(Guid id, UpdateForumThreadDto updateDto)
        {
            var thread = await _repository.GetByIdAsync(id);
            if (thread == null) throw new Exception("Thread not found");

            thread.Title = updateDto.Title;
            thread.Description = updateDto.Description;

            await _repository.UpdateAsync(thread);
            return MapToDto(thread);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var thread = await _repository.GetByIdAsync(id);
            if (thread == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ForumThreadDto MapToDto(ForumThread thread)
        {
            return new ForumThreadDto
            {
                Id = thread.Id,
                Title = thread.Title,
                Description = thread.Description,
                UserId = thread.UserId,
                UserName = thread.User?.UserName ?? "Unknown",
                CreatedAt = thread.CreatedAt
            };
        }
    }
}
