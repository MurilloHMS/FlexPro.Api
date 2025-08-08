using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class ContactMap : EntityBaseMap<Contato>
{
    public override void Configure(EntityTypeBuilder<Contato> builder)
    {
        base.Configure(builder);

        builder.ToTable("Contato");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnName("Nome");
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email");
        
        builder.Property(x => x.Outro)
            .IsRequired(false)
            .HasColumnName("outro");
        
        builder.Property(x => x.Mensagem)
            .IsRequired()
            .HasColumnName("Mensagem");
        
        builder.Property(x => x.NomeEmpresa)
            .IsRequired()
            .HasColumnName("NomeEmpresa");
    }
}