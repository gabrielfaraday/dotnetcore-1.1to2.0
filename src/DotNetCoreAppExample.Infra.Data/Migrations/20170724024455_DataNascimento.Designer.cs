using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DotNetCoreAppExample.Infra.Data.Context;

namespace DotNetCoreAppExample.Infra.Data.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20170724024455_DataNascimento")]
    partial class DataNascimento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetCoreAppExample.Domain.Contatos.Entities.Contato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AtivadoPor");

                    b.Property<bool>("Ativo");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<Guid?>("EnderecoId")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.ToTable("Contatos");
                });

            modelBuilder.Entity("DotNetCoreAppExample.Domain.Contatos.Entities.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("DotNetCoreAppExample.Domain.Contatos.Entities.Telefone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContatoId");

                    b.Property<int>("DDD");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ContatoId");

                    b.ToTable("Telefones");
                });

            modelBuilder.Entity("DotNetCoreAppExample.Domain.Contatos.Entities.Contato", b =>
                {
                    b.HasOne("DotNetCoreAppExample.Domain.Contatos.Entities.Endereco", "Endereco")
                        .WithOne("Contato")
                        .HasForeignKey("DotNetCoreAppExample.Domain.Contatos.Entities.Contato", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DotNetCoreAppExample.Domain.Contatos.Entities.Telefone", b =>
                {
                    b.HasOne("DotNetCoreAppExample.Domain.Contatos.Entities.Contato", "Contato")
                        .WithMany("Telefones")
                        .HasForeignKey("ContatoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
