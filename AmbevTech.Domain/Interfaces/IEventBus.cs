using AmbevTech.Domain.Events.Base;

namespace AmbevTech.Domain.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync(DomainEvent @event);
    }
}
