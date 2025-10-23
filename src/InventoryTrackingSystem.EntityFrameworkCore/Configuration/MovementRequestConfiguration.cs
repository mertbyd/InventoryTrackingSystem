using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class MovementRequestConfiguration:IEntityTypeConfiguration<MovementRequest>
{
    public void Configure(EntityTypeBuilder<MovementRequest> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "MovementRequests",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.RequesterUserId)
            .IsRequired();
        builder.Property(x => x.FromSiteId)
            .IsRequired();
        builder.Property(x => x.ToSiteId)
            .IsRequired();
        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("PENDING");
        // Foreign key ilişkileri
        builder.HasOne<Site>()
            .WithMany()
            .HasForeignKey(x => x.FromSiteId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Site>()
            .WithMany()
            .HasForeignKey(x => x.ToSiteId)
            .OnDelete(DeleteBehavior.Restrict);
        // Index"ler
        builder.HasIndex(x => x.RequesterUserId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.FromSiteId);
        builder.HasIndex(x => x.ToSiteId);
    }
}