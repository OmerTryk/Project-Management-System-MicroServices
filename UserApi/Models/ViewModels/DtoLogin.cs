using System.ComponentModel.DataAnnotations;

namespace UserApi.Models.ViewModels
{
    public class DtoLogin
    {
        public string NickName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
