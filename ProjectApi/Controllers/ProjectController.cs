using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Context;
using ProjectApi.Mappers;
using ProjectApi.Models;
using ProjectApi.Models.ViewModels;
using Shared.ProjectEvents;
using Shared.Settings;
using Shared.UserEvents;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly ProjectDbContext _context;
        readonly ISendEndpointProvider _sendEndpointProvider;

        public ProjectController(IMapper mapper, ProjectDbContext context, ISendEndpointProvider sendEndpointProvider)
        {
            _mapper = mapper;
            _context = context;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProject(DtoProjectUI dto)
        {
            var result = _mapper.MapToEntity(dto);

            ProjectStartedEvent projectStartedEvent = new()
            {
                CreatedDate = DateTime.UtcNow,
                ProjectId = result.Id,
                ProjectName = result.ProjectName,
                UserId = result.OwnerId,
                MembersMessages = dto.Members.Select(m => new Shared.Messages.ProjectMessage.ProjectMembersMessage
                {
                    Role = m.Role.ToString(),
                    MemberId = m.UserId
                })
            };
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.ProjectStateMachineQueue}"));
            await sendEndpoint.Send<ProjectStartedEvent>(projectStartedEvent);
            try
            {
                await _context.AddAsync(result);
                await _context.SaveChangesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Veritabanına kayıt sırasında hata oluştu: {ex.Message}");
            }

        }

        [HttpPost("getproject")]
        public async Task<IActionResult> GetProject(DtoGetProject userId)
        {
            var userProjects = await _context.ProjectMembers
         .Where(m => m.UserId == userId.Id)
         .Select(m => new
         {
             ProjectName = m.Project.ProjectName,
             ProjectDescription = m.Project.ProjectDescription,
             CreatedTime = m.Project.CreatedTime,
             OwnerId = m.Project.OwnerId,
             Members = m.Project.Members.Select(member => new
             {
                 member.UserId,
                 member.Role
             }).ToList()
         })
         .ToListAsync();

            if (!userProjects.Any())
            {
                return NotFound("Bu kullanıcıya ait proje bulunamadı!");
            }

            return Ok(userProjects);
        }
    }
}
