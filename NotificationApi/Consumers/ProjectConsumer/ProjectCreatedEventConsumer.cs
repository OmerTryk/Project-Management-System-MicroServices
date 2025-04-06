using MassTransit;
using NotificationApi.Context;
using NotificationApi.Models;
using Shared.ProjectEvents;

namespace NotificationApi.Consumers.ProjectConsumer
{
    public class ProjectCreatedEventConsumer : IConsumer<ProjectCreatedEvent>
    { 
        readonly NotificationDbContext _context;

        public ProjectCreatedEventConsumer(NotificationDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProjectCreatedEvent> context)
        {
            string message = $"{context.Message.ProjectName} adlı projeye {context.Message.MembersMessages} eklendiniz";
            ProjectNotification projectNotification = new()
            {
                Id = Guid.NewGuid(),
                ProjectId = context.Message.ProjectId,
                UserId = context.Message.UserId,
                ProjectContent = message,
                Members = context.Message.MembersMessages.Select(m=>new ProjectNotificationMember
                {
                    Id = Guid.NewGuid(),
                    MemberId = m.MemberId,
                    Role = m.Role
                }).ToList()
            };
            await _context.ProjectNotifications.AddAsync(projectNotification);
            await _context.SaveChangesAsync();
        }
    }
}
