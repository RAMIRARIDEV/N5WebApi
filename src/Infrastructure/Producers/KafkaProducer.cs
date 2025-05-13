using Confluent.Kafka;
using Microsoft.Extensions.Options;
using N5WebApi.Application.Abstractions;
using N5WebApi.Domain.Exceptions;
using N5WebApi.Infrastructure.Dtos;
using System.Text.Json;

namespace N5WebApi.Infrastructure.Producers;

internal sealed class KafkaProducer(ILogger<KafkaProducer> _logger, IOptions<KafkaSettings> kafkaOptions) : IEventPublisher
{
    public async Task SendAsync(string Topic, EventTask Event)
    {
        KafkaSettings kafkaSettings = kafkaOptions.Value;

        var config = new ProducerConfig
        {
            BootstrapServers = string.Concat(kafkaSettings.Hostname,":",kafkaSettings.Port),
        };

        var builder = new ProducerBuilder<string, string>(config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
        .Build();


        var eventMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(Event)
        };

        var deliveryStatus = await builder.ProduceAsync(Topic, eventMessage);

        if (deliveryStatus.Status == PersistenceStatus.NotPersisted)
        {
            _logger.LogError("Not Persisted error. Event Id: {Event.Id}", Event.Id);
            throw new Exception(GenerateEventErrors.CreateEventError.ToString());
        }
    }
}
