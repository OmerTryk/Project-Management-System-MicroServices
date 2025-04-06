using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Settings
{
    public static class RabbitMQSettings
    {
        public const string StateMachineQueue = $"state-machine-queue";
        public const string ProjectStateMachineQueue = $"project-state-machine-queue";
        public const string UserCreatedEventQueue = $"user-created-event-queue";
        public const string ProjectCreatedEventQueue = $"project-created-event-queue";
    }
}
