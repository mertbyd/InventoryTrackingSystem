using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class SiteConfiguration:IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "Sites+",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Address)
            .HasMaxLength(255);
        builder.HasIndex(x => x.Type);
    }
}