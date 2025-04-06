using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Shared.UserEvents
{
    public class UserCreatedEvent : CorrelatedBy<Guid>
    {
        public UserCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid CorrelationId { get; }
        public Guid UserId { get; set; }
        public string? NickName { get; set; }
    }
}
