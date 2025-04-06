using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using SagaStateMachineService.StateInstance;
using Shared.Settings;
using Shared.UserEvents;

namespace SagaStateMachineService.StateMachine
{
    public class UserStateMachine : MassTransitStateMachine<UserStateInstance>
    {
        public Event<UserStartedEvent> UserStartedEvent { get; set; }
        public State UserCreated { get; set; }

        public UserStateMachine()
        {
            InstanceState(instance => instance.CurrentState);

            #region Event
            Event(() => UserStartedEvent,
                    userstateInstance => userstateInstance.CorrelateBy<Guid>(database => database.UserId, @event => @event.Message.UserId).SelectId(e => Guid.NewGuid()));
            #endregion

            Initially(When(UserStartedEvent).
                Then(_context =>
                {
                    _context.Saga.UserId = _context.Message.UserId;
                    _context.Saga.CreatedDate = DateTime.UtcNow;
                }).
                TransitionTo(UserCreated).
                Send(new Uri($"queue:{RabbitMQSettings.UserCreatedEventQueue}"),
                context => new UserCreatedEvent(context.Saga.CorrelationId)
                {
                    NickName = context.Message.NickName,
                    UserId = context.Message.UserId
                }));
        }
    }
}
