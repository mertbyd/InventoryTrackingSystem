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
using System; // <--- ADDED THIS LINE TO FIX CS0246 and CS0103

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
    static InventoryTrackingSystemEntityFrameworkCoreModule()
    {
        // OLD, OBSOLETE NPGSQL GLOBAL TYPE MAPPING CODE WAS LIKELY HERE (Lines 61-63 in the original context)
        // IT MUST BE REMOVED/COMMENTED OUT.
        
        ConfigureDbSchemasStatic();
    }
    
    private static void ConfigureDbSchemasStatic()
    {
        // ABP modules go to "abp" schema
        Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = "abp";
        Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = "abp";
        Volo.Abp.SettingManagement.AbpSettingManagementDbProperties.DbSchema = "abp";
        Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = "abp";
        Volo.Abp.BackgroundJobs.AbpBackgroundJobsDbProperties.DbSchema = "abp";
        Volo.Abp.FeatureManagement.AbpFeatureManagementDbProperties.DbSchema = "abp";
        Volo.Abp.TenantManagement.AbpTenantManagementDbProperties.DbSchema = "abp";
        
        // OpenIddict goes to "openiddict" schema
        Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = "openiddict";
        
        // Your domain entities go to "inventory" schema
        InventoryTrackingSystemDbProperties.DbSchema = "inventory";
    }
    
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        ConfigureDbSchemas(configuration);
    }
    
    private void ConfigureDbSchemas(IConfiguration configuration)
    {
        // Set default schemas
        ConfigureDbSchemasStatic();
        
        // Allow override from appsettings.json
        var schemas = configuration.GetSection("EntityFrameworkCore:Schemas");
        if (schemas.Exists())
        {
            var identitySchema = schemas["Volo.Abp.Identity"];
            if (!string.IsNullOrEmpty(identitySchema))
                Volo.Abp.Identity.AbpIdentityDbProperties.DbSchema = identitySchema;
            var permissionSchema = schemas["Volo.Abp.PermissionManagement"];
            if (!string.IsNullOrEmpty(permissionSchema))
                Volo.Abp.PermissionManagement.AbpPermissionManagementDbProperties.DbSchema = permissionSchema;
            var settingSchema = schemas["Volo.Abp.SettingManagement"];
            if (!string.IsNullOrEmpty(settingSchema))
                Volo.Abp.SettingManagement.AbpSettingManagementDbProperties.DbSchema = settingSchema;
            var auditSchema = schemas["Volo.Abp.AuditLogging"];
            if (!string.IsNullOrEmpty(auditSchema))
                Volo.Abp.AuditLogging.AbpAuditLoggingDbProperties.DbSchema = auditSchema;
            var openIddictSchema = schemas["Volo.Abp.OpenIddict"];
            if (!string.IsNullOrEmpty(openIddictSchema))
                Volo.Abp.OpenIddict.AbpOpenIddictDbProperties.DbSchema = openIddictSchema;
            var tenantSchema = schemas["Volo.Abp.TenantManagement"];
            if (!string.IsNullOrEmpty(tenantSchema))
                Volo.Abp.TenantManagement.AbpTenantManagementDbProperties.DbSchema = tenantSchema;
            var bgJobsSchema = schemas["Volo.Abp.BackgroundJobs"];
            if (!string.IsNullOrEmpty(bgJobsSchema))
                Volo.Abp.BackgroundJobs.AbpBackgroundJobsDbProperties.DbSchema = bgJobsSchema;
            var featureSchema = schemas["Volo.Abp.FeatureManagement"];
            if (!string.IsNullOrEmpty(featureSchema))
                Volo.Abp.FeatureManagement.AbpFeatureManagementDbProperties.DbSchema = featureSchema;
            var inventorySchema = schemas["InventoryTrackingSystem"];
            if (!string.IsNullOrEmpty(inventorySchema))
                InventoryTrackingSystemDbProperties.DbSchema = inventorySchema;
        }
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Configure PostgreSQL
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseNpgsql();
        });
        
        // Add DbContext and repositories
        context.Services.AddAbpDbContext<InventoryTrackingSystemDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
            
            /* Add custom repositories here. Example:
             * options.AddRepository<Inventory, EfCoreInventoryRepository>();
             */
        });
    }
}