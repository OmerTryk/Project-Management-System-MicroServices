using UserApi.Models;
using UserApi.Models.ViewModels;

namespace UserApi.Mappers
{
    public interface IUserMapper
    {
        User MaptoUser(DtoUserUI userdto);
        DtoUserGet MapToDto (User users);
        IEnumerable<DtoUserGet> MapToDtos(IEnumerable<User> user);
    }
}
