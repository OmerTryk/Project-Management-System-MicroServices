namespace PMS_Frontend.Models.ViewModels.MessageVM
{
    public class MessagePageVM
    {
        public MessageCreateVM NewMessage { get; set; }
        public List<MessageListVM> PreviousMessages { get; set; }
    }
}
