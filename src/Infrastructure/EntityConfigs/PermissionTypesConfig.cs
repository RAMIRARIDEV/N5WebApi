using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5WebApi.src.Domain.Models;

namespace N5WebApi.src.Infrastructure.Configs;

internal class PermissionTypesConfig : IEntityTypeConfiguration<PermissionTypes>
{
    public void Configure(EntityTypeBuilder<PermissionTypes> entity)
    {
        entity.ToTable("PermissionTypes");
        entity.HasKey(e => e.Id).HasName("PK__Permissi__3214EC07D1F5B84C");

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();


        entity.Property(e => e.CreationDate)
        .HasColumnType("datetime");

        entity.Property(e => e.ModificationDate)
        .HasColumnType("datetime");
    }
}