using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5WebApi.src.Domain.Models;

namespace N5WebApi.src.Infrastructure.Configs;

internal class PermissionsConfig : IEntityTypeConfiguration<Permissions>
{
    public void Configure(EntityTypeBuilder<Permissions> entity)
    {
        entity.ToTable("Permissions");
        entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC074F9002FD");

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        entity.HasOne(p => p.PermissionsTypes)
            .WithMany(p => p.Permissions)
            .HasForeignKey(p => p.PermissionType)
            .HasConstraintName("FK_Permissions_PermissionTypes");
    }
}
