namespace PMS_Frontend.Models.ViewModels.ProjectVM
{
    public class DtoGetProjectMember
    {
        public Guid UserId { get; set; }
        public string? UserNickName { get; set; }
        public string Role { get; set; }
    }
}
