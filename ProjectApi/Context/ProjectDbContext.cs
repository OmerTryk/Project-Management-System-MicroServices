using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi.Context
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
    }
}
