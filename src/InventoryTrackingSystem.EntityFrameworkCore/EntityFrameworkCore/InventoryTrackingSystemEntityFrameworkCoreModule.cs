using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace InventoryTrackingSystem.EntityFrameworkCore;

[DependsOn(
    typeof(InventoryTrackingSystemDomainModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
)]
public class InventoryTrackingSystemEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        ConfigureDbSchemas(configuration);
    }
    
    private void ConfigureDbSchemas(IConfiguration configuration)
    {
        // SADECE appsettings.json"dan oku
        var schemas = configuration.GetSection("EntityFrameworkCore:Schemas");
        // Identity
        Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = 
            schemas["Volo.Abp.Identity"] ?? null;
        // Permission Management
        Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = 
            schemas["Volo.Abp.PermissionManagement"] ?? null;
        // Setting Management
        Volo.Abp.SettingManagement.AbpSettingManagementDbProperties.DbSchema = 
            schemas["Volo.Abp.SettingManagement"] ?? null;
        // Audit Logging
        Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = 
            schemas["Volo.Abp.AuditLogging"] ?? null;
        // Background Jobs
        Volo.Abp.BackgroundJobs.AbpBackgroundJobsDbProperties.DbSchema = 
            schemas["Volo.Abp.BackgroundJobs"] ?? null;
        // Feature Management
        Volo.Abp.FeatureManagement.AbpFeatureManagementDbProperties.DbSchema = 
            schemas["Volo.Abp.FeatureManagement"] ?? null;
        // Tenant Management
        Volo.Abp.TenantManagement.AbpTenantManagementDbProperties.DbSchema = 
            schemas["Volo.Abp.TenantManagement"] ?? null;
        // OpenIddict
        Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = 
            schemas["Volo.Abp.OpenIddict"] ?? null;
        // Your Domain
        InventoryTrackingSystemDbProperties.DbSchema = 
            schemas["InventoryTrackingSystem"] ?? null;
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        
        context.Services.AddAbpDbContext<InventoryTrackingSystemDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });
    }
}