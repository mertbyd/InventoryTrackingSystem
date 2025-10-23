using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InventoryTrackingSystem.EntityFrameworkCore;

public class InventoryTrackingSystemHttpApiHostMigrationsDbContext : AbpDbContext<InventoryTrackingSystemHttpApiHostMigrationsDbContext>
{
    public InventoryTrackingSystemHttpApiHostMigrationsDbContext(DbContextOptions<InventoryTrackingSystemHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureInventoryTrackingSystem();
    }
}
