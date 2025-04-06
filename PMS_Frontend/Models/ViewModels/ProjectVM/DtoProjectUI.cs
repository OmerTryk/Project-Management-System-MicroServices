namespace PMS_Frontend.Models.ViewModels.ProjectVM
{
    public class DtoProjectUI
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Guid OwnerId { get; set; }
        public IEnumerable<DtoProjectMember>? Members { get; set; }
    }
}
