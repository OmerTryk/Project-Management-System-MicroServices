namespace NotificationApi.Models
{
    public class ProjectNotification
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string ProjectContent { get; set; }
        public ICollection<ProjectNotificationMember> Members { get; set; }
    }
}
