using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Domain.Core;

namespace DotNetCoreAppExample.Domain.Usuarios.Services
{
    public class UsuarioDadosService : ServiceBase<UsuarioDados, IUsuarioDadosRepository>, IUsuarioDadosService
    {
        public UsuarioDadosService(IUsuarioDadosRepository repository) : base(repository)
        {
        }
    }
}