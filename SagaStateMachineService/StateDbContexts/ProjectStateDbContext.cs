using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineService.StateInstance;
using SagaStateMachineService.StateMaps;

namespace SagaStateMachineService.StateDbContexts
{
    public class ProjectStateDbContext : SagaDbContext
    {
        public ProjectStateDbContext(DbContextOptions<ProjectStateDbContext> options) : base(options)
        {
        }
        public DbSet<ProjectStateInstance> ProjectStateInstances { get; set; }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new ProjectStateMap();
            }
        }
    }
}
