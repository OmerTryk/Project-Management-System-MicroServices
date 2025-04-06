using MassTransit;

namespace SagaStateMachineService.StateInstance
{
    public class UserStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public string? CurrentState { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

