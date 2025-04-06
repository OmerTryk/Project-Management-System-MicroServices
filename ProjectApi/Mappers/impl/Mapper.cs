using ProjectApi.Models;
using ProjectApi.Models.ViewModels;

namespace ProjectApi.Mappers.impl
{
    public class Mapper : IMapper
    {
        public DtoProjectUI MapToDto(Project entity)
        {
            DtoProjectUI dto = new()
            {
                ProjectName = entity.ProjectName,
                ProjectDescription = entity.ProjectDescription,
                OwnerId = entity.OwnerId
            };
            return dto;
        }

        public Project MapToEntity(DtoProjectUI dto)
        {
            Project entity = new()
            {
                Id = Guid.NewGuid(),
                CreatedTime = DateTime.UtcNow,
                ProjectDescription = dto.ProjectDescription,
                OwnerId = dto.OwnerId,
                ProjectName = dto.ProjectName
            };

            entity.Members = dto.Members.Select(m => new ProjectMember
            {
                AssignedAt = DateTime.UtcNow,
                Role = m.Role,
                UserId = m.UserId
            }).ToList();

            return entity;
        }
    }
}
