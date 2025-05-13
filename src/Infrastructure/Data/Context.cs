using Microsoft.EntityFrameworkCore;
using N5WebApi.src.Infrastructure.Configs;
using N5WebApi.src.Domain.Models;

namespace N5WebApi.src.Infrastructure.Data;
public partial class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured){}
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionTypesConfig());
        modelBuilder.ApplyConfiguration(new PermissionsConfig());
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public virtual DbSet<PermissionTypes> PermissionTypes { get; set; }
    public virtual DbSet<Permissions> Permissions { get; set; }
}