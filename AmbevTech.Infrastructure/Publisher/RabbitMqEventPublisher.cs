﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tingle.EventBus;

namespace AmbevTech.Infrastructure.Publisher
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task<ScheduledResult?> PublishAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEvent>(EventContext<TEvent> @event, DateTimeOffset? scheduled = null, CancellationToken cancellationToken = default) where TEvent : class
        {
            var eventName = @event.GetType().Name;
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: eventName, basicProperties: null, body: body);

            return Task.FromResult(new ScheduledResult?());
        }

        public Task<IList<ScheduledResult>?> PublishAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEvent>(IList<EventContext<TEvent>> events, DateTimeOffset? scheduled = null, CancellationToken cancellationToken = default) where TEvent : class
        {
            throw new NotImplementedException();
        }


        public Task CancelAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEvent>(string id, CancellationToken cancellationToken = default) where TEvent : class
        {
            throw new NotImplementedException();
        }

        public Task CancelAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEvent>(IList<string> ids, CancellationToken cancellationToken = default) where TEvent : class
        {
            throw new NotImplementedException();
        }

        public EventContext<TEvent> CreateEventContext<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TEvent>(TEvent @event, string? correlationId = null) where TEvent : class
        {
            throw new NotImplementedException();
        }
    }

}
