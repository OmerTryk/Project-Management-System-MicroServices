using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messages.ProjectMessage
{
    public class ProjectMembersMessage
    {
        public Guid MemberId { get; set; }
        public string Role { get; set; }
    }
}
