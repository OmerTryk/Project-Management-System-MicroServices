using System.ComponentModel.DataAnnotations;

namespace PMS_Frontend.Models.ViewModels.UserVM
{
    public class DtoLogin
    {
        [Required(ErrorMessage = "Kullanıcı adı gereklidir.")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
