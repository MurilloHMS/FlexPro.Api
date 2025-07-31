using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class VehicleMap : EntityBaseMap<Veiculo>
{
    public override void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        base.Configure(builder);

        builder.ToTable("Veiculos");

        builder.HasKey(x => x.Id);
    }
}