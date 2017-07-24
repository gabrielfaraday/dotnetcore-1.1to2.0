using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using DotNetCoreAppExample.Domain.Core;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Contatos.Services
{
    public class ContatoService : ServiceBase<Contato, IContatoRepository>, IContatoService
    {
        readonly IUser _user;

        public ContatoService(IContatoRepository contatoRepository, IUser user) : base(contatoRepository)
        {
            _user = user;
        }

        public override Contato Add(Contato contato)
        {
            contato.AtivarContato(_user.GetUserId()); //Obter usuário logado

            return base.Add(contato);
        }

        public ICollection<Contato> ObterAtivos()
        {
            return _repository.ObterContatosAtivos();
        }

        public Contato ObterPorEmail(string email)
        {
            return _repository.ObterContatoPorEmail(email);
        }

        public Telefone AdicionarTelefone(Telefone telefone)
        {
            if (!telefone.EstaValido())
                return telefone;

            return _repository.AdicionarTelefone(telefone);
        }

        public Telefone AtualizarTelefone(Telefone telefone)
        {
            if (!telefone.EstaValido())
                return telefone;

            return _repository.AtualizarTelefone(telefone);
        }

        public Telefone ObterTelefonePorId(Guid telefoneId)
        {
            return _repository.ObterTelefonePorId(telefoneId);
        }

        public void RemoverTelefone(Guid telefoneId)
        {
            _repository.RemoverTelefone(telefoneId);
        }

        public void RemoverEndereco(Guid enderecoId)
        {
            _repository.RemoverEndereco(enderecoId);
        }

        public override void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
