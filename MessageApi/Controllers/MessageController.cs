using MessageApi.Context;
using MessageApi.Model;
using MessageApi.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        readonly MessageDbContext _context;

        public MessageController(MessageDbContext context)
        {
            _context = context;
        }

        [HttpGet("getmessage")]
        public async Task<IActionResult> GetMessageById([FromQuery] Guid id)
        {
            var messages = await _context.Messages
       .Where(m => m.ReceiverId == id || m.SenderId == id)
       .ToListAsync();
            return Ok(messages);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageCreateDto messageDto)
        {
            if (messageDto == null)
            {
                return BadRequest();
            }
            Message message = new()
            {
                Content = messageDto.Content,
                Id = Guid.NewGuid(),
                ReceiverId = messageDto.ReceiverId,
                SenderId = messageDto.SenderId,
                Timestamp = DateTime.UtcNow
            };
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

