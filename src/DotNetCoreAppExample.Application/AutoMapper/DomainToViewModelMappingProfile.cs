using AutoMapper;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Domain.Contatos.Entities;

namespace DotNetCoreAppExample.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Contato, ContatoViewModel>();
            CreateMap<Telefone, TelefoneViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
        }
    }
}