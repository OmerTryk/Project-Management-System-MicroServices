using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Messages.ProjectMessage;

namespace Shared.ProjectEvents
{
    public class ProjectStartedEvent
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public IEnumerable<ProjectMembersMessage> MembersMessages { get; set; }

    }
}
