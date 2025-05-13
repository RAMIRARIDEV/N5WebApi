namespace N5WebApi.Infrastructure.Dtos;

internal sealed class KafkaSettings
{
    public string Hostname { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
}
