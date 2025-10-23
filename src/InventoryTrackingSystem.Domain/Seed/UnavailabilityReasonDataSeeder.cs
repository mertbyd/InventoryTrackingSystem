using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Microsoft.Extensions.Logging;
using InventoryTrackingSystem.Entities;

namespace InventoryTrackingSystem.Seed;

public class UnavailabilityReasonDataSeeder : IDataSeeder, ITransientDependency
{
    private readonly IRepository<UnavailabilityReason, Guid> _unavailabilityReasonRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<UnavailabilityReasonDataSeeder> _logger;
    public UnavailabilityReasonDataSeeder(
        IRepository<UnavailabilityReason, Guid> unavailabilityReasonRepository,
        IUnitOfWorkManager unitOfWorkManager,
        ILogger<UnavailabilityReasonDataSeeder> logger)
    {
        _unavailabilityReasonRepository = unavailabilityReasonRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }
    public async Task SeedAsync(DataSeedContext context = null)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            await CreateUnavailabilityReasonIfNotExistsAsync(
                Guid.Parse("88888888-8888-8888-8888-888888888888"),
                "ARIZA"
            );

            await CreateUnavailabilityReasonIfNotExistsAsync(
                Guid.Parse("99999999-9999-9999-9999-999999999999"),
                "BAKIM"
            );

            await CreateUnavailabilityReasonIfNotExistsAsync(
                Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                "KAYIP"
            );

            await CreateUnavailabilityReasonIfNotExistsAsync(
                Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                "REZERVE"
            );

            await CreateUnavailabilityReasonIfNotExistsAsync(
                Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                "HASARLI"
            );

            await uow.CompleteAsync();
            _logger.LogInformation("Unavailability Reasons seeded successfully!");
        }
    }
    private async Task CreateUnavailabilityReasonIfNotExistsAsync(Guid id, string reason)
    {
        var existingReason = await _unavailabilityReasonRepository.FindAsync(id);
        if (existingReason == null)
        {
            var unavailabilityReason = new UnavailabilityReason(id, reason);
            await _unavailabilityReasonRepository.InsertAsync(unavailabilityReason, autoSave: false);
            _logger.LogInformation($"Unavailability Reason {reason} created successfully!");
        }
        else
        {
            _logger.LogInformation($"Unavailability Reason {reason} already exists.");
        }
    }
}