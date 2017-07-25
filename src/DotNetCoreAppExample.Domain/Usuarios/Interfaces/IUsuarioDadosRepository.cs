using DotNetCoreAppExample.Domain.Core.Interfaces;
using DotNetCoreAppExample.Domain.Usuarios.Entities;

namespace DotNetCoreAppExample.Domain.Usuarios.Interfaces
{
    public interface IUsuarioDadosRepository : IRepositoryBase<UsuarioDados>
    {
        UsuarioDados ObterUsuarioPorCPF(string cpf);
    }
}
