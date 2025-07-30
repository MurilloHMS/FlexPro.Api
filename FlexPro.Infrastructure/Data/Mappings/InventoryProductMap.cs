using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class InventoryProductMap : EntityBaseMap<Products>
{
    public override void Configure(EntityTypeBuilder<Products> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("inventory-products");

        builder.HasKey(p => p.Id);

        builder.Property(x => x.Nome)
            .IsRequired(true)
            .HasMaxLength(200)
            .HasColumnType("TEXT")
            .HasColumnName("name");
        
        builder.Property(x => x.SystemCode)
            .IsRequired(true)
            .HasMaxLength(8)
            .HasColumnType("TEXT")
            .HasColumnName("system_code");
        
        builder.Property(x => x.MinimumStock)
            .IsRequired(false)
            .HasColumnName("minimum_stock");
    }
}