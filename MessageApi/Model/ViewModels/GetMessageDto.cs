namespace MessageApi.Model.ViewModels
{
    public class GetMessageDto
    {
        public string SenderNickName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
    }
}
