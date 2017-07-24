using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreAppExample.Infra.Data.Mappings
{
    public class UsuarioDadosMapping : EntityTypeConfiguration<UsuarioDados>
    {
        public override void Map(EntityTypeBuilder<UsuarioDados> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(e => e.CPF)
               .HasColumnType("varchar(11)")
               .HasMaxLength(11)
               .IsRequired();

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("UsuarioDados");
        }
    }
}