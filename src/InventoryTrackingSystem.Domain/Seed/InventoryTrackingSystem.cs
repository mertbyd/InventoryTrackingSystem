using System;
using System.Threading.Tasks;
using InventoryTrackingSystem.Seed;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace InventoryTrackingSystem.Seed;
public class InventoryTrackingSystemDataSeeder : IDataSeeder, ITransientDependency
{
    private readonly SiteDataSeeder _siteSeeder;
    private readonly VehicleDataSeeder _vehicleSeeder;
    private readonly UnavailabilityReasonDataSeeder _unavailabilityReasonSeeder;
    private readonly SerialNumberDataSeeder _serialNumberSeeder; // YENİ
    private readonly ILogger<InventoryTrackingSystemDataSeeder> _logger;
    public InventoryTrackingSystemDataSeeder(
        SiteDataSeeder siteSeeder,
        VehicleDataSeeder vehicleSeeder,
        UnavailabilityReasonDataSeeder unavailabilityReasonSeeder,
        SerialNumberDataSeeder serialNumberSeeder, // YENİ
        ILogger<InventoryTrackingSystemDataSeeder> logger)
    {
        _siteSeeder = siteSeeder;
        _vehicleSeeder = vehicleSeeder;
        _unavailabilityReasonSeeder = unavailabilityReasonSeeder;
        _serialNumberSeeder = serialNumberSeeder; // YENİ
        _logger = logger;
    }
    public async Task SeedAsync(DataSeedContext context = null)
    {
        context ??= new DataSeedContext();
        try
        {
            _logger.LogInformation("Starting lookup data seeding...");
            // Lookup tabloları seed et
            await _siteSeeder.SeedAsync(context);
            await _vehicleSeeder.SeedAsync(context);
            await _unavailabilityReasonSeeder.SeedAsync(context);
            await _serialNumberSeeder.SeedAsync(context); // YENİ
            _logger.LogInformation("Lookup data seeding completed successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during lookup data seeding!");
            throw;
        }
    }
}