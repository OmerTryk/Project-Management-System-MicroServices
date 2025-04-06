namespace NotificationApi.Models
{
    public class UserNotification
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string? NickName { get; set; }
        public string? Content { get; set; }

    }
}
