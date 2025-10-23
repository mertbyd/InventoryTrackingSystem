using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InventoryTrackingSystem.Entities;

namespace InventoryTrackingSystem.EntityFrameworkCore.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "Stocks",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.SerialNumberId)
            .IsRequired();
        builder.Property(x => x.TotalProduct)
            .IsRequired()
            .HasDefaultValue(0);
        builder.Property(x => x.AvailableProduct)
            .IsRequired()
            .HasDefaultValue(0);
        // Foreign key ilişkisi
        builder.HasOne<SerialNumber>()
            .WithMany()
            .HasForeignKey(x => x.SerialNumberId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(x => x.SerialNumberId).IsUnique();
    }
}