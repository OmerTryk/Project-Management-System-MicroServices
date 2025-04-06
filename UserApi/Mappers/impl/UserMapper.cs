using UserApi.Models;
using UserApi.Models.ViewModels;

namespace UserApi.Mappers.impl
{
    public class UserMapper : IUserMapper
    {
        public DtoUserGet MapToDto(User user)
        {
            DtoUserGet dto = new()
            {
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName,
               NickName = user.NickName
            };

            return dto;
        }

        public IEnumerable<DtoUserGet> MapToDtos(IEnumerable<User> users)
        {
            var userDtos = new List<DtoUserGet>();

            foreach (var user in users)
            {
                var userDto = new DtoUserGet
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    NickName = user.NickName,
                    Email = user.Email
                };

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        public User MaptoUser(DtoUserUI userdto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                NickName= userdto.NickName,
                Email = userdto.Email,
                CreatedDate = DateTime.Now,
                HashPassword = userdto.HashPassword,
            };

            return user;
        }
    }
}
