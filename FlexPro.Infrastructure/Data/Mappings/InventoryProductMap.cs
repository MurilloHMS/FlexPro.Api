using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class InventoryProductMap : EntityBaseMap<InventoryProducts>
{
    public override void Configure(EntityTypeBuilder<InventoryProducts> builder)
    {
        base.Configure(builder);

        builder.ToTable("inventory_products");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnType("TEXT")
            .HasColumnName("name");

        builder.Property(x => x.SystemCode)
            .IsRequired()
            .HasMaxLength(8)
            .HasColumnType("TEXT")
            .HasColumnName("system_code");

        builder.Property(x => x.MinimumStock)
            .IsRequired(false)
            .HasColumnName("minimum_stock")
            .HasColumnType("int");
        
        // Relacionamento 1:N
        builder.HasMany(p => p.Movements)
            .WithOne(m => m.InventoryProduct)
            .HasForeignKey(m => m.SystemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}