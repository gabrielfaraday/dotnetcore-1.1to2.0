using DotNetCoreAppExample.Domain.Contatos.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using DotNetCoreAppExample.Infra.Data.Extensions;
using DotNetCoreAppExample.Infra.Data.Mappings;
using System.Linq;
using System.Reflection;
using System;
using DotNetCoreAppExample.Domain.Usuarios.Entities;

namespace DotNetCoreAppExample.Infra.Data.Context
{
    public class MainContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        public DbSet<UsuarioDados> UsuarioDados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new ContatoMapping());
            modelBuilder.AddConfiguration(new EnderecoMapping());
            modelBuilder.AddConfiguration(new TelefoneMapping());

            modelBuilder.AddConfiguration(new UsuarioDadosMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        public override int SaveChanges()
        {
            foreach(var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property("DataCadastro").IsModified = false;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
