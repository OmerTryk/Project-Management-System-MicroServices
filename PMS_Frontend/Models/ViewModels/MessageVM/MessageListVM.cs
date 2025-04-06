namespace PMS_Frontend.Models.ViewModels.MessageVM
{
    public class MessageListVM
    {
        public Guid SenderId { get; set; }
        public string SenderNickName { get; set; }
        public string Content { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
