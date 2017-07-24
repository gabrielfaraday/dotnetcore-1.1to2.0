using AutoMapper;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Domain.Contatos.Entities;

namespace DotNetCoreAppExample.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ContatoViewModel, Contato>()
                .ConstructUsing(c => new Contato(c.Id, c.Nome, c.Email, c.DataNascimento));

            CreateMap<TelefoneViewModel, Telefone>()
                .ConstructUsing(t => new Telefone(t.Id, t.DDD, t.Numero, t.ContatoId));

            CreateMap<EnderecoViewModel, Endereco>()
                .ConstructUsing(e => new Endereco(e.Id, e.Logradouro, e.Numero, e.Complemento, e.Bairro, e.CEP, e.Cidade, e.Estado));
        }
    }
}