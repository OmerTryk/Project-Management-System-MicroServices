namespace ProjectApi.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedTime { get; set; }
        public IEnumerable<ProjectMember>? Members { get; set; }
    }
}
