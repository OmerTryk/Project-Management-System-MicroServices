namespace PMS_Frontend.Models.ViewModels.ProjectVM
{
    public class DtoGetProjectContent
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Guid OwnerId { get; set; }
        public string? OwnerNickName { get; set; }
        public DateTime CreatedTime { get; set; }
        public IEnumerable<DtoGetProjectMember>? Members { get; set; }
    }
}
