using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using InventoryTrackingSystem.Entities;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;

namespace InventoryTrackingSystem.EntityFrameworkCore;

[ConnectionStringName(InventoryTrackingSystemDbProperties.ConnectionStringName)]
public class InventoryTrackingSystemDbContext : AbpDbContext<InventoryTrackingSystemDbContext>, IInventoryTrackingSystemDbContext
{
    // Lookups
    public DbSet<Site> Sites { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<UnavailabilityReason> UnavailabilityReasons { get; set; }
    public DbSet<SerialNumber> SerialNumbers { get; set; }
    
    // Domain
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<MovementRequest> MovementRequests { get; set; }
    public DbSet<MovementRequestDetail> MovementRequestDetails { get; set; }
    public DbSet<MovementRequestResponse> MovementRequestResponses { get; set; }
    public DbSet<InventoryMovement> InventoryMovements { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    
    // Audit
    public DbSet<InventoryHistory> InventoryHistories { get; set; }
    
    public InventoryTrackingSystemDbContext(DbContextOptions<InventoryTrackingSystemDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // ABP modüllerini configure et
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ConfigureBackgroundJobs();
        
        // Kendi entity'lerinizi configure et
        builder.ConfigureInventoryTrackingSystem();
    }
}