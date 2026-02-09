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
    public class ForumPostService : IForumPostService
    {
        private readonly IForumPostRepository _repository;

        public ForumPostService(IForumPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<ForumPostDto?> GetByIdAsync(Guid id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null) return null;

            return MapToDto(post);
        }

        public async Task<IEnumerable<ForumPostDto>> GetByThreadIdAsync(Guid threadId)
        {
            var posts = await _repository.GetByThreadIdAsync(threadId);
            var allDtos = posts.Select(MapToDto).ToList();
            
            // Build hierarchy
            var rootPosts = allDtos.Where(p => p.ParentPostId == null).ToList();
            foreach (var post in allDtos)
            {
                post.Replies = allDtos.Where(r => r.ParentPostId == post.Id).ToList();
            }
            
            return rootPosts;
        }

        public async Task<ForumPostDto> CreateAsync(CreateForumPostDto createDto, Guid userId)
        {
            var post = new ForumPost
            {
                Id = Guid.NewGuid(),
                Content = createDto.Content,
                ForumThreadId = createDto.ForumThreadId,
                UserId = userId,
                ParentPostId = createDto.ParentPostId,
                CreatedAt = DateTime.UtcNow,
                IsHelpful = false
            };

            await _repository.AddAsync(post);
            
            // Reload to get navigation properties (like User)
            var createdPost = await _repository.GetByIdAsync(post.Id);
            return MapToDto(createdPost!);
        }

        public async Task<ForumPostDto> UpdateAsync(Guid id, UpdateForumPostDto updateDto)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null) throw new Exception("Post not found");

            post.Content = updateDto.Content;
            post.IsHelpful = updateDto.IsHelpful;

            await _repository.UpdateAsync(post);
            return MapToDto(post);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ForumPostDto MapToDto(ForumPost post)
        {
            return new ForumPostDto
            {
                Id = post.Id,
                Content = post.Content,
                ForumThreadId = post.ForumThreadId,
                UserId = post.UserId,
                UserName = post.User?.UserName ?? "Unknown",
                ParentPostId = post.ParentPostId,
                IsHelpful = post.IsHelpful,
                VoteCount = post.VoteCount,
                CreatedAt = post.CreatedAt
            };
        }
    }
}
