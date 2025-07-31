using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class CategoryMap : EntityBaseMap<Categoria>
{
    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Nome)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(50);
    }
}