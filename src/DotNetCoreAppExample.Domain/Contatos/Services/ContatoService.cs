using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using DotNetCoreAppExample.Domain.Core;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Contatos.Services
{
    public class ContatoService : ServiceBase<Contato, IContatoRepository>, IContatoService
    {
        public ContatoService(IContatoRepository contatoRepository) : base(contatoRepository)
        {
        }

        public override Contato Add(Contato contato)
        {
            if (string.IsNullOrWhiteSpace(contato.Endereco.Logradouro) &&
                    string.IsNullOrWhiteSpace(contato.Endereco.Bairro) &&
                        string.IsNullOrWhiteSpace(contato.Endereco.Cidade) &&
                            string.IsNullOrWhiteSpace(contato.Endereco.CEP))
                contato.LimparEndereco();

            contato.AtivarContato();

            return base.Add(contato);
        }

        public override Contato Update(Contato contato)
        {
            if (string.IsNullOrWhiteSpace(contato.Endereco.Logradouro) &&
                    string.IsNullOrWhiteSpace(contato.Endereco.Bairro) &&
                        string.IsNullOrWhiteSpace(contato.Endereco.Cidade) &&
                            string.IsNullOrWhiteSpace(contato.Endereco.CEP))
                contato.LimparEndereco();

            return base.Update(contato);
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

        public Endereco ObterEnderecoPorId(Guid enderecoId)
        {
            return _repository.ObterEnderecoPorId(enderecoId);
        }

        public Endereco AdicionarEndereco(Endereco endereco)
        {
            return _repository.AdicionarEndereco(endereco);
        }

        public Endereco AtualizarEndereco(Endereco endereco)
        {
            return _repository.AtualizarEndereco(endereco);
        }

        public override void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose();
        }
    }
}
