using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class EmailMap : EntityBaseMap<Email>
{
    public override void Configure(EntityTypeBuilder<Email> builder)
    {
        base.Configure(builder);

        builder.ToTable("emails_smtp");

        builder.Property(x => x.Address)
            .HasMaxLength(50)
            .HasColumnName("email_address")
            .IsRequired();

        builder.Property(x => x.IsEnabled)
            .HasColumnName("is_enabled");
    }
}