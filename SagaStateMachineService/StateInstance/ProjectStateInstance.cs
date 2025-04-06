using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace SagaStateMachineService.StateInstance
{
    public class ProjectStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public Guid ProjectId { get; set; }
        public string? CurrentState { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
