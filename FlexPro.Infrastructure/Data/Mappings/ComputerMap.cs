using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class ComputerMap : EntityBaseMap<Computador>
{
    public override void Configure(EntityTypeBuilder<Computador> builder)
    {
        builder.HasOne(c => c.Especificacoes)
            .WithOne(e => e.Computador)
            .HasForeignKey<Especificacoes>(x => x.IdComputador);
    }
}