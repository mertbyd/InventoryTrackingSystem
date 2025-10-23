using InventoryTrackingSystem.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryTrackingSystem.Configuration;

public class MovementRequestResponseConfiguration: IEntityTypeConfiguration<MovementRequestResponse>
{
    public void Configure(EntityTypeBuilder<MovementRequestResponse> builder)
    {
        builder.ToTable(InventoryTrackingSystemDbProperties.DbTablePrefix + "MovementRequestResponses",
            InventoryTrackingSystemDbProperties.DbSchema);
        builder.Property(x => x.MovementRequestId)
            .IsRequired();
        builder.Property(x => x.ApproverUserId)
            .IsRequired();
        builder.Property(x => x.IsApproved)
            .IsRequired();
        builder.Property(x => x.Comment)
            .HasMaxLength(255);
        // Foreign key ilişkisi - Her talep için tek response
        builder.HasOne<MovementRequest>()
            .WithMany()
            .HasForeignKey(x => x.MovementRequestId)
            .OnDelete(DeleteBehavior.Cascade);
        // Index"ler
        builder.HasIndex(x => x.MovementRequestId).IsUnique(); // Her talep için tek onay
        builder.HasIndex(x => x.ApproverUserId);
    }
}