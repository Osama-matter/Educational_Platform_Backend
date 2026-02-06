using EducationalPlatform.Application.DTOs.Forum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Interfaces.Services
{
    public interface IForumSubscriptionService
    {
        Task<IEnumerable<ForumSubscriptionDto>> GetUserSubscriptionsAsync(Guid userId);
        Task<ForumSubscriptionDto> SubscribeAsync(Guid userId, CreateForumSubscriptionDto subscribeDto);
        Task<bool> UnsubscribeAsync(Guid userId, Guid threadId);
        Task<bool> IsSubscribedAsync(Guid userId, Guid threadId);
    }
}
