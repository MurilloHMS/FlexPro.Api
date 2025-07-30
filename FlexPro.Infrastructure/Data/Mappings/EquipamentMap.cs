using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class EquipamentMap : EntityBaseMap<Equipamento>
{
    public override void Configure(EntityTypeBuilder<Equipamento> builder)
    {
        base.Configure(builder);
        
        builder.HasDiscriminator<string>("Tipo")
            .HasValue<Computador>("Computador");
    }
}