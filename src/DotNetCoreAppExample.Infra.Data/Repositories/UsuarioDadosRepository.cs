using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using DotNetCoreAppExample.Infra.Data.Context;

namespace DotNetCoreAppExample.Infra.Data.Repositories
{
    public class UsuarioDadosRepository : RepositoryBase<UsuarioDados>, IUsuarioDadosRepository
    {
        public UsuarioDadosRepository(MainContext mainContext) : base(mainContext)
        {
        }
    }
}
