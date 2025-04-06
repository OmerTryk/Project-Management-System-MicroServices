namespace ProjectApi.Models.ViewModels
{
    public class DtoProjectMember
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
