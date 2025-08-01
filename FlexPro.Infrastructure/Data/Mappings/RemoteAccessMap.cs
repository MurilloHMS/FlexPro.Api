using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class RemoteAccessMap : EntityBaseMap<AcessoRemoto>
{
    public override void Configure(EntityTypeBuilder<AcessoRemoto> builder)
    {
        base.Configure(builder);

        builder.HasOne(a => a.Computador)
            .WithMany(c => c.AcessosRemotos)
            .HasForeignKey(x => x.IdComputador);
    }
}