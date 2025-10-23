using System;
using InventoryTrackingSystem.Entities;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection; // Gerekli using eklendi

namespace InventoryTrackingSystem.Seed;

// Hata çözüldü: ITransientDependency arayüzü eklenerek DI sistemine otomatik kayıt sağlandı.
public class VehicleDataSeeder : ITransientDependency
{
    private readonly IRepository<Vehicle, Guid> _vehicleRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<VehicleDataSeeder>   _logger;  
    
    public VehicleDataSeeder(
        IRepository<Vehicle, Guid> vehicleRepository,
        IUnitOfWorkManager unitOfWorkManager,
        ILogger<VehicleDataSeeder> logger)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }
    
    public async Task SeedAsync(DataSeedContext context = null)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            await CreateVehicleIfNotExistsAsync(
                Guid.Parse("55555555-5555-5555-5555-555555555555"),
                "34ABC123",
                "KAMYONET"
            );

            await CreateVehicleIfNotExistsAsync(
                Guid.Parse("66666666-6666-6666-6666-666666666666"),
                "06DEF456",
                "KAMYON"
            );

            await CreateVehicleIfNotExistsAsync(
                Guid.Parse("77777777-7777-7777-7777-777777777777"),
                "35GHI789",
                "TIR"
            );

            await CreateVehicleIfNotExistsAsync(
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-111111111111"),
                "34XYZ987",
                "BINEK"
            );

            await uow.CompleteAsync();
            _logger.LogInformation("Vehicles seeded successfully!");
        }
    }
    
    private async Task CreateVehicleIfNotExistsAsync(Guid id, string plateNumber, string type)
    {
        var existingVehicle = await _vehicleRepository.FindAsync(id);
        if (existingVehicle == null)
        {
            var vehicle = new Vehicle(id, plateNumber, type);
            await _vehicleRepository.InsertAsync(vehicle, autoSave: false);
            _logger.LogInformation($"Vehicle {plateNumber} - {type} created successfully!");
        }
        else
        {
            _logger.LogInformation($"Vehicle {plateNumber} already exists.");
        }
    }
}
