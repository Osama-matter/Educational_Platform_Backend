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
    public class ForumSubscriptionService : IForumSubscriptionService
    {
        private readonly IForumSubscriptionRepository _repository;
        private readonly IForumThreadRepository _threadRepository;
        private readonly IUserRepository _userRepository;
        public ForumSubscriptionService(IForumSubscriptionRepository repository, IForumThreadRepository threadRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _threadRepository = threadRepository;
            _userRepository = userRepository;

        }

        public async Task<IEnumerable<ForumSubscriptionDto>> GetUserSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _repository.GetByUserIdAsync(userId);
            return subscriptions.Select(s => new ForumSubscriptionDto
            {
                Id = s.Id,
                ForumThreadId = s.ForumThreadId,
                UserId = s.UserId,
                ForumThreadTitle = s.Forumthread?.Title ?? "Unknown Thread"
            });
        }

        public async Task<ForumSubscriptionDto> SubscribeAsync(Guid userId, CreateForumSubscriptionDto subscribeDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if(user == null) throw new Exception("User not found");

            var thread = await _threadRepository.GetByIdAsync(subscribeDto.ForumThreadId);
            if(thread == null) throw new Exception("Thread not found");

            var existing = await _repository.GetByUserAndThreadAsync(userId, subscribeDto.ForumThreadId);
            if (existing != null)
            {
                return new ForumSubscriptionDto
                {
                    Id = existing.Id,
                    ForumThreadId = existing.ForumThreadId,
                    UserId = existing.UserId,
                    ForumThreadTitle = existing.Forumthread?.Title ?? thread.Title
                };
            }

            var subscription = new ForumSubscriptions
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ForumThreadId = subscribeDto.ForumThreadId
            };

            await _repository.AddAsync(subscription);
            
            var created = await _repository.GetByIdAsync(subscription.Id);
            return new ForumSubscriptionDto
            {
                Id = created!.Id,
                ForumThreadId = created.ForumThreadId,
                UserId = created.UserId,
                ForumThreadTitle = created.Forumthread?.Title ?? "Unknown Thread"
            };
        }

        public async Task<bool> UnsubscribeAsync(Guid userId, Guid threadId)
        {
            var subscription = await _repository.GetByUserAndThreadAsync(userId, threadId);
            if (subscription == null) return false;

            await _repository.DeleteAsync(subscription.Id);
            return true;
        }

        public async Task<bool> IsSubscribedAsync(Guid userId, Guid threadId)
        {
            var subscription = await _repository.GetByUserAndThreadAsync(userId, threadId);
            return subscription != null;
        }
    }
}
