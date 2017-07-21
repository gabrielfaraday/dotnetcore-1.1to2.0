using AutoMapper;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Domain.Contatos.Entities;

namespace DotNetCoreAppExample.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<NovoContatoViewModel, Contato>()
                .ConstructUsing(c => new Contato(null, c.Nome, c.Email));

            CreateMap<ContatoViewModel, Contato>()
                .ConstructUsing(c => new Contato(c.Id, c.Nome, c.Email));

            CreateMap<NovoTelefoneViewModel, Telefone>()
                .ConstructUsing(t => new Telefone(null, t.DDD, t.Numero, t.ContatoId));

            CreateMap<TelefoneViewModel, Telefone>()
                .ConstructUsing(t => new Telefone(t.Id, t.DDD, t.Numero, t.ContatoId));

            CreateMap<NovoEnderecoViewModel, Endereco>()
                .ConstructUsing(e => new Endereco(null, e.Logradouro, e.Numero, e.Complemento, e.Bairro, e.CEP, e.Cidade, e.Estado, e.ContatoId));

            CreateMap<EnderecoViewModel, Endereco>()
                .ConstructUsing(e => new Endereco(e.Id, e.Logradouro, e.Numero, e.Complemento, e.Bairro, e.CEP, e.Cidade, e.Estado, e.ContatoId));
        }
    }
}