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
            builder.Property(c => c.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Ignore(c => c.ValidationResult);

            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Categorias");
        }
    }
}