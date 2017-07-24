using DotNetCoreAppExample.Application.Interfaces;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using AutoMapper;
using DotNetCoreAppExample.Domain.Core.Interfaces;

namespace DotNetCoreAppExample.Application.Services
{
    public class UsuarioDadosAppService : AppServiceBase<UsuarioDados, UsuarioDadosViewModel, IUsuarioDadosService>, IUsuarioDadosAppService
    {
        public UsuarioDadosAppService(IUnitOfWork uow, IUsuarioDadosService service, IMapper mapper) : base(uow, service, mapper)
        {
        }
    }
}
