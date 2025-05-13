using N5WebApi.Infrastructure.Dtos;

namespace N5WebApi.Application.Abstractions;

internal interface IEventPublisher
{
    Task SendAsync(string Topic, EventTask Event);
}
