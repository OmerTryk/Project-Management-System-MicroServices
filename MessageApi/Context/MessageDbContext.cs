using MessageApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Context
{
    public class MessageDbContext : DbContext
    {
        public MessageDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
    }
}
