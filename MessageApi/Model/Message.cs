using System.ComponentModel.DataAnnotations;

namespace MessageApi.Model
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string? Content { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
