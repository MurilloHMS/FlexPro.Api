using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class PrimaryMaterialReceiptMap : EntityBaseMap<ReceitaMateriaPrima>
{
    public override void Configure(EntityTypeBuilder<ReceitaMateriaPrima> builder)
    {
        base.Configure(builder);

        builder.HasKey(rm => new { rm.ReceitaId, rm.MateriaPrimaId });

        builder.HasOne(rm => rm.Receita)
            .WithMany(r => r.ReceitaMateriaPrima)
            .HasForeignKey(rm => rm.ReceitaId);

        builder.HasOne(rm => rm.MateriaPrima)
            .WithMany(r => r.ReceitaMateriaPrima)
            .HasForeignKey(rm => rm.MateriaPrimaId);
    }
}