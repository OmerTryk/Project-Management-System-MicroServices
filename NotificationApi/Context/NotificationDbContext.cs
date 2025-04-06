using Microsoft.EntityFrameworkCore;
using NotificationApi.Models;

namespace NotificationApi.Context
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<ProjectNotification> ProjectNotifications { get; set; }
        public DbSet<ProjectNotificationMember> ProjectNotificationMembers { get; set; }
    }
}
