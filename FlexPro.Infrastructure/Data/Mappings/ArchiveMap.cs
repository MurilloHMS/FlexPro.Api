using FlexPro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexPro.Infrastructure.Data.Mappings;

public class ArchiveMap : EntityBaseMap<Arquivo>
{
    public override void Configure(EntityTypeBuilder<Arquivo> builder)
    {
        base.Configure(builder);

        builder.ToTable("archives");

        builder.Property(x => x.Name)
            .HasColumnName("file_name")
            .HasMaxLength(100)
            .HasColumnType("varchar")
            .IsRequired();

        builder.Property(x => x.Extensions)
            .HasColumnName("file_extensions")
            .HasMaxLength(6)
            .HasColumnType("varchar")
            .IsRequired(false);

        builder.Property(x => x.Size)
            .HasColumnName("file_size")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.IsPublic)
            .HasColumnName("is_public")
            .HasColumnType("boolean")
            .IsRequired();
    }
}