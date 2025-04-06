using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationApi.Context;
using NotificationApi.Models;

namespace NotificationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        readonly NotificationDbContext _context;

        public NotificationController(NotificationDbContext context)
        {
            _context = context;
        }

        [HttpGet("notifications/{NickName}")]
        public async Task<IActionResult> GetUserNotifications(string NickName)
        {
            var notification = _context.UserNotifications.Where(n => n.NickName == NickName).ToList();
            return Ok(notification);
        }
    }
}
