namespace UserApi.Models.ViewModels
{
    public class UserCheckResultDto
    {
        public Dictionary<string, Guid> UserIds { get; set; } = new();
        public ICollection<string>? NotFoundUsers { get; set; }
    }
}
