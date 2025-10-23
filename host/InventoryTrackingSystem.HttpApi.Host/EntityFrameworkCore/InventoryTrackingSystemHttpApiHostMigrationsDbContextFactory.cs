using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InventoryTrackingSystem.EntityFrameworkCore;

public class InventoryTrackingSystemHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<InventoryTrackingSystemHttpApiHostMigrationsDbContext>
{
    public InventoryTrackingSystemHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<InventoryTrackingSystemHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("InventoryTrackingSystem"));

        return new InventoryTrackingSystemHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
