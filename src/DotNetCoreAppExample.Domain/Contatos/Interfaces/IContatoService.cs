using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Contatos.Interfaces
{
    public interface IContatoService : IServiceBase<Contato>
    {
        Contato ObterPorEmail(string email);
        ICollection<Contato> ObterAtivos();

        void RemoverEndereco(Guid enderecoId);

        Telefone AdicionarTelefone(Telefone telefone);
        Telefone AtualizarTelefone(Telefone telefone);
        Telefone ObterTelefonePorId(Guid telefoneId);
        void RemoverTelefone(Guid telefoneId);
    }
}
