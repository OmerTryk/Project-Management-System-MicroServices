namespace PMS_Frontend.Models.ViewModels.ProjectVM
{
    public class DtoPostProject
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Guid OwnerId { get; set; }
        public IEnumerable<DtoPostMember>? Members { get; set; }
    }
}
