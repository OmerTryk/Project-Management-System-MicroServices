using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.Settings;
using Shared.UserEvents;
using UserApi.Context;
using UserApi.Mappers;
using UserApi.Models;
using UserApi.Models.ViewModels;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserDbContext _context;

        readonly IUserMapper _mapper;

        readonly ISendEndpointProvider _sendendpointprovider;
        public UserController(UserDbContext context, IUserMapper mapper, ISendEndpointProvider sendendpointprovider)
        {
            _context = context;
            _mapper = mapper;
            _sendendpointprovider = sendendpointprovider;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(DtoUserUI userDto)
        {
            var user = _mapper.MaptoUser(userDto);
            UserStartedEvent userStartedEvent = new()
            {
                UserId = user.Id,
                NickName = user.NickName
            };

            var sendEndpoint = await _sendendpointprovider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.StateMachineQueue}"));
            await sendEndpoint.Send<UserStartedEvent>(userStartedEvent);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DtoLogin login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NickName == login.NickName && u.HashPassword == login.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre" });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AlliturnambizimelevarirsanSekersöylekaymaksöylebalsöyle"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token için claims (Kullanıcı bilgileri)
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.NickName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            // Token oluşturuluyor
            var token = new JwtSecurityToken(
                issuer: "userapi.com",  // Token'ı hangi servis üretiyor
                audience: "userapi.com", // Token'ı hangi servisler kullanabilir
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),  // Token süresi
                signingCredentials: creds
            );

            // Token'ı string olarak döndürüyoruz
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("getuserbynickname")]
        public async Task<IActionResult> GetUserByNickName(DtoUserNickName dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.NickName == dto.Nickname);
            if (user != null)
            {
                return Ok(user.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("checkmembers")]
        public async Task<IActionResult> CheckMembers(DtoCheckMembers dto)
        {
            if (dto == null || dto.Nickname == null || !dto.Nickname.Any())
            {
                return BadRequest("Boş liste gönderilemez.");
            }
            var users = await _context.Users
        .Where(u => dto.Nickname.Contains(u.NickName))
        .Select(u => new { u.NickName, u.Id })
        .ToListAsync();

            var foundUsers = users.ToDictionary(u => u.NickName, u => u.Id);

            var notFoundUsers = dto.Nickname.Except(foundUsers.Keys).ToList();

            var result = new UserCheckResultDto
            {
                UserIds = foundUsers,
                NotFoundUsers = notFoundUsers
            };

            if (notFoundUsers.Any())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("getusersnickname")]
        public async Task<IActionResult> GetUsersNickName([FromBody]IEnumerable<Guid> userIds)
        {
            if (userIds == null || !userIds.Any())
            {
                return BadRequest("Kullanıcı Idleri boş veya null");
            }
            var nicknames = await _context.Users
       .Where(u => userIds.Contains(u.Id))
       .Select(u => new {u.Id,u.NickName})
       .ToListAsync();

            return Ok(nicknames);
        }
    }
}
