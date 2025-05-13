namespace N5WebApi.Infrastructure.Dtos;

internal sealed class EventTask(string OperationName)
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string OperationName { get; set; } = OperationName;
}
