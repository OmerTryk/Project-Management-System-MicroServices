using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.UserEvents
{
    public class UserStartedEvent
    {
        public Guid UserId { get; set; }
        public string? NickName { get; set; }
    }
}
