using AutoMapper;
using DotNetCoreAppExample.Application.Interfaces;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Services
{
    public class ContatoAppService : AppServiceBase<Contato, ContatoViewModel, IContatoService>, IContatoAppService
    {
        public ContatoAppService(IContatoService contatoService, IUnitOfWork uow, IMapper mapper) : base(uow, contatoService, mapper)
        {
        }

        public ICollection<ContatoViewModel> ObterAtivos()
        {
            return _mapper.Map<ICollection<ContatoViewModel>>(_service.ObterAtivos());
        }

        public ContatoViewModel ObterPorEmail(string email)
        {
            return _mapper.Map<ContatoViewModel>(_service.ObterPorEmail(email));
        }

        public TelefoneViewModel AdicionarTelefone(TelefoneViewModel telefoneViewModel)
        {
            throw new Exception("ERRO GABRIEL");
            var telefone = _service.AdicionarTelefone(_mapper.Map<Telefone>(telefoneViewModel));

            if (telefone.ValidationResult.IsValid)
                Commit();

            return _mapper.Map<TelefoneViewModel>(telefone);
        }

        public TelefoneViewModel AtualizarTelefone(TelefoneViewModel telefoneViewModel)
        {
            var telefone = _service.AtualizarTelefone(_mapper.Map<Telefone>(telefoneViewModel));

            if (telefone.ValidationResult.IsValid)
                Commit();

            return _mapper.Map<TelefoneViewModel>(telefone);
        }

        public void RemoverTelefone(Guid id)
        {
            _service.RemoverTelefone(id);
            Commit();
        }

        public TelefoneViewModel ObterTelefonePorId(Guid id)
        {
            return _mapper.Map<TelefoneViewModel>(_service.ObterTelefonePorId(id));
        }

        public void RemoverEndereco(Guid enderecoId)
        {
            _service.RemoverEndereco(enderecoId);
            Commit();
        }
    }
}
