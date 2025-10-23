using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryTrackingSystem.Entities;
namespace InventoryTrackingSystem.EntityFrameworkCore.Configurations;
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "Inventories",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.SerialNumberId)
            .IsRequired();
        builder.Property(x => x.CurrentSiteId)
            .IsRequired();
        builder.Property(x => x.IsAvailableForRequest)
            .HasDefaultValue(true);
        // Foreign key ilişkileri
        builder.HasOne<SerialNumber>()
            .WithMany()
            .HasForeignKey(x => x.SerialNumberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Site>()
            .WithMany()
            .HasForeignKey(x => x.CurrentSiteId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<UnavailabilityReason>()
            .WithMany()
            .HasForeignKey(x => x.UnavailabilityReasonId)
            .OnDelete(DeleteBehavior.SetNull);
        // Index"ler
        builder.HasIndex(x => x.SerialNumberId);
        builder.HasIndex(x => x.CurrentSiteId);
        builder.HasIndex(x => x.IsAvailableForRequest);
    }
}