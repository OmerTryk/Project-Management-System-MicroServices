using MassTransit;
using System.Text.Json;
using System.Text;
using Shared.UserEvents;
using NotificationApi.Context;
using NotificationApi.Models;

namespace NotificationApi.Consumers.UserConsumers
{
    public class UserCreatedEventConsumer : IConsumer<UserCreatedEvent>
    {
        readonly NotificationDbContext _context;

        public UserCreatedEventConsumer(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            string content = $"Hoşgeldin {context.Message.NickName}";
            var notification = new UserNotification
            {
                Id = Guid.NewGuid(),
                Content = content,
                OwnerId = context.Message.UserId,
                NickName = context.Message.NickName
            };

            await _context.UserNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }
    }
}
