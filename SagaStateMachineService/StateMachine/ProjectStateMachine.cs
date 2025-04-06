using MassTransit;
using SagaStateMachineService.StateInstance;
using Shared.ProjectEvents;
using Shared.Settings;

namespace SagaStateMachineService.StateMachine
{
    public class ProjectStateMachine : MassTransitStateMachine<ProjectStateInstance>
    {
        public Event<ProjectStartedEvent> ProjectStartedEvent { get; set; }
        public State ProjectCreated { get; set; }

        public ProjectStateMachine()
        {
            InstanceState(instance => instance.CurrentState);

            Event(() => ProjectStartedEvent,
                projectstartedevent=>projectstartedevent.CorrelateBy<Guid>(database => database.ProjectId,@event=>
                @event.Message.ProjectId).SelectId(e=>Guid.NewGuid()));

            Initially(When(ProjectStartedEvent).
                Then(_context =>
                {
                    _context.Saga.ProjectId = _context.Message.ProjectId;
                    _context.Saga.CreatedDate = DateTime.UtcNow;
                }).
                TransitionTo(ProjectCreated).
                Send(new Uri($"queue:{RabbitMQSettings.ProjectCreatedEventQueue}"),
                context => new ProjectCreatedEvent(context.Saga.CorrelationId)
                {
                    ProjectName = context.Message.ProjectName,
                    ProjectId = context.Message.ProjectId,
                    UserId = context.Message.UserId,
                    MembersMessages = context.Message.MembersMessages
                }));
        }
    }
}
