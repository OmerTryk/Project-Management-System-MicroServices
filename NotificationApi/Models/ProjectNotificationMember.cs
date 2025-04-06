namespace NotificationApi.Models
{
    public class ProjectNotificationMember
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid ProjectNotificationId { get; set; }
        public string Role { get; set; }
    }
}
