using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreAppExample.Infra.Data.Mappings
{
    public class ContatoMapping : EntityTypeConfiguration<Contato>
    {
        public override void Map(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(c => c.Email)
               .HasColumnType("varchar(256)")
               .IsRequired();

            builder.Property(c => c.DataCadastro)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Ativo)
               .IsRequired();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Contato)
                .HasForeignKey<Contato>(e => e.EnderecoId)
                .IsRequired();

            builder.ToTable("Contatos");
        }
    }
}