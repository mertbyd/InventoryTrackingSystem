using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class UnavailabilityReasonConfiguration : IEntityTypeConfiguration<UnavailabilityReason>
{
    public void Configure(EntityTypeBuilder<UnavailabilityReason> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "UnavailabilityReasons",
            InventoryTrackingSystemDbProperties.DbSchema);
        
        builder.Property(x => x.Reason)
            .IsRequired()
            .HasMaxLength(100);
        
        // Reason unique olmalı
        builder.HasIndex(x => x.Reason).IsUnique();
    }
}