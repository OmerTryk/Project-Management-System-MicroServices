namespace MessageApi.Model.ViewModels
{
    public class MessageCreateDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
