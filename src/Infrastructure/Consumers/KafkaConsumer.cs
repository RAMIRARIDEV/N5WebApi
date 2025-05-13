using N5WebApi.Application.Abstractions;

namespace N5WebApi.Infrastructure.Consumers;

internal sealed class KafkaConsumer(IServiceProvider _serviceProvider) : IHostedService
{

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //logger
        IServiceScope scope = _serviceProvider.CreateScope();
        var @event = scope.ServiceProvider.GetRequiredService<IEventConsumer>();

        Task.Run( () => @event.Consume("KAFKA_TOPIC"), cancellationToken);
            
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
