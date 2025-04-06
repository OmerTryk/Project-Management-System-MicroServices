namespace ProjectApi.Models.ViewModels
{
    public class DtoProjectUI
    {
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Guid OwnerId { get; set; }
        public IEnumerable<DtoProjectMember>? Members { get; set; }
    }
}
