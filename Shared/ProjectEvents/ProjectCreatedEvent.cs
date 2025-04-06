using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Shared.Messages.ProjectMessage;

namespace Shared.ProjectEvents
{
    public class ProjectCreatedEvent : CorrelatedBy<Guid>
    {
        public ProjectCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId {  get; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<ProjectMembersMessage> MembersMessages { get; set; }
    }
}
