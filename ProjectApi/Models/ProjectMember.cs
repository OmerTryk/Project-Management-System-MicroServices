using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ProjectApi.Models
{
    public class ProjectMember
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public DateTime AssignedAt { get; set; }
        public Project Project { get; set; }
    }
}
