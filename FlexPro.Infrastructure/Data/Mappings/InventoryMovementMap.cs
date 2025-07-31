using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class InventoryMovementMap : EntityBaseMap<InventoryMovement>
{
    public override void Configure(EntityTypeBuilder<InventoryMovement> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("inventory_movements");
        
        builder.Property(x => x.SystemId)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("system_code")
            .HasColumnType("varchar");

        builder.Property(x => x.Data)
            .HasColumnName("date")
            .IsRequired(false)
            .HasColumnType("date");

        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired()
            .HasColumnType("int");
    }
}