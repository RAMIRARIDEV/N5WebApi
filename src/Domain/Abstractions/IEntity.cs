namespace N5WebApi.Domain.Abstractions;

public interface IEntity
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
    public void SetModificationDate();
}
