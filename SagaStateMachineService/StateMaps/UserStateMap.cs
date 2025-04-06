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
    public class UserStateMap : SagaClassMap<UserStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<UserStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.UserId).
                IsRequired();
        }
    }
}
