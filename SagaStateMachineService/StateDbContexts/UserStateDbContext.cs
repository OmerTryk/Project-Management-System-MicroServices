using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineService.StateInstance;
using SagaStateMachineService.StateMaps;

namespace SagaStateMachineService.StateDbContexts
{
    public class UserStateDbContext : SagaDbContext
    {
        public UserStateDbContext(DbContextOptions<UserStateDbContext> options) : base(options)
        {
        }
        public DbSet<UserStateInstance> UserStateInstances{ get; set; }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get
            {
                yield return new UserStateMap();
            }
        }
    }
}
