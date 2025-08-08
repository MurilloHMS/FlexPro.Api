using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class EntityMap : EntityBaseMap<Entidade>
{
    public override void Configure(EntityTypeBuilder<Entidade> builder)
    {
        base.Configure(builder);

        builder.ToTable("Entidade");

        builder.HasDiscriminator<string>("Tipo")
            .HasValue<Vendedor>("Vendedor")
            .HasValue<Cliente>("Cliente")
            .HasValue<Parceiro>("Parceiro")
            .HasValue<PrestadorDeServico>("PrestSer");
    }
}