using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreAppExample.Infra.Data.Mappings
{
    public class EnderecoMapping : EntityTypeConfiguration<Endereco>
    {
        public override void Map(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            builder.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnType("varchar(8)");

            builder.Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Enderecos");
        }
    }
}