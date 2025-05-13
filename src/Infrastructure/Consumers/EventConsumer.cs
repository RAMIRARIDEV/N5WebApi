using Confluent.Kafka;
using Microsoft.Extensions.Options;
using N5WebApi.Application.Abstractions;
using N5WebApi.Domain.Exceptions;
using N5WebApi.Infrastructure.Dtos;
using System.Text.Json;

namespace N5WebApi.Infrastructure.Consumers;

internal sealed class EventConsumer(IOptions<ConsumerConfig> _config, IServiceScopeFactory _serviceProvider) : IEventConsumer
{

    public void Consume(string Topic)
    {
        var consumer = new ConsumerBuilder<string, string>(_config.Value)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetKeyDeserializer(Deserializers.Utf8)
            .Build();

        consumer.Subscribe(Topic);

        while (true)
        {
            var consumeResult = consumer.Consume();
            if (consumeResult is null) continue;
            if (consumeResult.Message is null) continue;

            var @event = JsonSerializer.Deserialize<EventTask>(consumeResult.Message.Value);
        
            if (@event is null) throw new Exception(GenerateEventErrors.ConsumerEventError.ToString());

            consumer.Commit(consumeResult);
        }
    }
}
