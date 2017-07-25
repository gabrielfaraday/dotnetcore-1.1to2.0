using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using DotNetCoreAppExample.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Linq;

namespace DotNetCoreAppExample.Infra.Data.Repositories
{
    public class UsuarioDadosRepository : RepositoryBase<UsuarioDados>, IUsuarioDadosRepository
    {
        public UsuarioDadosRepository(MainContext mainContext) : base(mainContext)
        {
        }

        public UsuarioDados ObterUsuarioPorCPF(string cpf)
        {
            var sql = @"SELECT * FROM USUARIODADOS WHERE CPF = @pcpf";

            var usuario = Db.Database.GetDbConnection().Query<UsuarioDados>(sql, new { pcpf = cpf });

            return usuario.FirstOrDefault();
        }
    }
}
