using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class PackagingMap : EntityBaseMap<Embalagem>
{
    public override void Configure(EntityTypeBuilder<Embalagem> builder)
    {
        base.Configure(builder);

        builder.HasOne(e => e.ProdutoLoja)
            .WithMany(p => p.Embalagems)
            .HasForeignKey(e => e.ProdutoLojaId);
    }
}