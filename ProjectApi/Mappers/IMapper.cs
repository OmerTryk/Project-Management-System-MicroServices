using ProjectApi.Models;
using ProjectApi.Models.ViewModels;

namespace ProjectApi.Mappers
{
    public interface IMapper
    {
        Project MapToEntity(DtoProjectUI dto);
        DtoProjectUI MapToDto(Project entity);
    }
}
