using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineService.StateInstance;

namespace SagaStateMachineService.StateMaps
{
    public class ProjectStateMap : SagaClassMap<ProjectStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<ProjectStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.ProjectId).
                IsRequired();
        }
    }
}
