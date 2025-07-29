using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class InventoryProductMap : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.ToTable("inventory-products");

        builder.HasKey(p => p.Id);

        builder.Property(x => x.Nome)
            .IsRequired(true)
            .HasMaxLength(200)
            .HasColumnType("NVARCHAR")
            .HasColumnName("name");
        
        builder.Property(x => x.SystemCode)
            .IsRequired(true)
            .HasMaxLength(8)
            .HasColumnType("NVARCHAR")
            .HasColumnName("system_code");
        
        builder.Property(x => x.MinimumStock)
            .IsRequired(false)
            .HasColumnName("minimum_stock");
    }
}