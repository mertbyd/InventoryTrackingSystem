using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Microsoft.Extensions.Logging;
using InventoryTrackingSystem.Entities;

namespace InventoryTrackingSystem.Seed;

public class SerialNumberDataSeeder : IDataSeeder, ITransientDependency
{
    private readonly IRepository<SerialNumber, Guid> _serialNumberRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<SerialNumberDataSeeder> _logger;

    public SerialNumberDataSeeder(
        IRepository<SerialNumber, Guid> serialNumberRepository,
        IUnitOfWorkManager unitOfWorkManager,
        ILogger<SerialNumberDataSeeder> logger)
    {
        _serialNumberRepository = serialNumberRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }

    public async Task SeedAsync(DataSeedContext context = null)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            await CreateSerialNumberIfNotExistsAsync(
                Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                "Kürek",
                "A"
            );
            await CreateSerialNumberIfNotExistsAsync(
                Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                "Levha",
                "B"
            );
            await CreateSerialNumberIfNotExistsAsync(
                Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                "Kazma",
                "C"
            );
            await CreateSerialNumberIfNotExistsAsync(
                Guid.Parse("00000000-1111-1111-1111-111111111111"),
                "Çekiç",
                "D"
            );
            await CreateSerialNumberIfNotExistsAsync(
                Guid.Parse("00000000-2222-2222-2222-222222222222"),
                "Testere",
                "E"
            );
            await uow.CompleteAsync();
            _logger.LogInformation("Serial Numbers seeded successfully!");
        }
    }
    private async Task CreateSerialNumberIfNotExistsAsync(Guid id, string name, string serialNumberPrefix)
    {
        var existingSerialNumber = await _serialNumberRepository.FindAsync(id);
        if (existingSerialNumber == null)
        {
            var serialNumber = new SerialNumber(id, name, serialNumberPrefix);
            await _serialNumberRepository.InsertAsync(serialNumber, autoSave: false);
            _logger.LogInformation($"Serial Number {name}  {serialNumberPrefix} created successfully!");
        }
        else
        {
            _logger.LogInformation($"Serial Number {name} already exists.");
        }
    }
}