namespace N5WebApi.Domain.Abstractions;

public class Entity : IEntity
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public DateTime? ModificationDate { get; set; }
    public void SetModificationDate() => ModificationDate = DateTime.Now;
}