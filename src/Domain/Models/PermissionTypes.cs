using N5WebApi.Domain.Abstractions;

namespace N5WebApi.src.Domain.Models;

public partial class PermissionTypes(string Description) : Entity
{
    public PermissionTypes() : this(string.Empty) { }
    public string Description { get; private set; } = Description;
    public virtual ICollection<Permissions> Permissions { get; private set; } = new List<Permissions>();
    public void ChangeDescription(string Description)
    {
        this.Description = Description;
        SetModificationDate();
    }
}
