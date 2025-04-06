namespace PMS_Frontend.Models.ViewModels.MessageVM
{
    public class MessageCreateDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
