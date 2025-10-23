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
public class SiteDataSeeder : ITransientDependency 
{
    private readonly IRepository<Site,Guid> _siteRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ILogger<SiteDataSeeder> _logger;

    public SiteDataSeeder(
        IRepository<Site, Guid> siteRepository,
        IUnitOfWorkManager unitOfWorkManager,
        ILogger<SiteDataSeeder> logger)
    {
        _siteRepository = siteRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _logger = logger;
    }

    public async Task SeedAsync(DataSeedContext context = null)
    {
        using (var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
        {
            await CreateSiteIfNotExistsAsync(
                Guid.Parse("11111111-1111-1111-1111-111111111111"),
                "WAREHOUSE",
                "Merkez Depo - İstanbul Hadımköy"
            );
            // Sahalar
            await CreateSiteIfNotExistsAsync(
                Guid.Parse("22222222-2222-2222-2222-222222222222"),
                "FIELD",
                "Ankara Şantiyesi - Çankaya"
            );  await CreateSiteIfNotExistsAsync(
                Guid.Parse("33333333-3333-3333-3333-333333333333"),
                "FIELD",
                "İzmir Şantiyesi - Karşıyaka"
            );
            await uow.CompleteAsync();
            _logger.LogInformation("Sites seeded successfully!");
            
        }
    }

    private async Task CreateSiteIfNotExistsAsync(Guid id, string type, string address)
    {
        var existingSite = await _siteRepository.FindAsync(id);
        if (existingSite == null)
        {
            var site = new Site(id, type, address);
            await _siteRepository.InsertAsync(site, autoSave: false);
            _logger.LogInformation($"Site '{type} - {address}' created successfully!");
        }   
        else
        {
            _logger.LogInformation($"Site '{type}' already exists.");
        }
    }
}
