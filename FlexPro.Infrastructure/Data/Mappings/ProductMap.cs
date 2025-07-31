using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class ProductMap : EntityBaseMap<Produto>
{
    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        base.Configure(builder);

        builder.ToTable("produto");

        builder.HasDiscriminator<string>("Tipo")
            .HasValue<ProdutoLoja>("ProdutoLoja")
            .HasValue<MateriaPrima>("materiaPrima");
    }
}