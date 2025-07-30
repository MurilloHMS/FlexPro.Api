using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class VehicleReviewMap : EntityBaseMap<Revisao>
{
    public override void Configure(EntityTypeBuilder<Revisao> builder)
    {
        base.Configure(builder);
        
        builder.HasOne(r => r.Local)
            .WithMany()
            .HasForeignKey(r => r.LocalId);

       builder.HasOne(r => r.Veiculo)
            .WithMany()
            .HasForeignKey(r => r.VeiculoId);
    }
}