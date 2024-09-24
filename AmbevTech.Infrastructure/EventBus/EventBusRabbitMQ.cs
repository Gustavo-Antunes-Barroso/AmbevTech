using AmbevTech.Domain.Events.Base;
using AmbevTech.Domain.Interfaces;
using Tingle.EventBus;

namespace AmbevTech.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IEventPublisher _eventPublisher;

        public EventBusRabbitMQ(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public async Task PublishAsync(DomainEvent @event)
        {
            var eventContext = _eventPublisher.CreateEventContext(@event);
            await _eventPublisher.PublishAsync(eventContext);
        }
    }
}
