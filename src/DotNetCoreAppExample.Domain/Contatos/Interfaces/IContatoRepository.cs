using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Contatos.Interfaces
{
    public interface IContatoRepository : IRepositoryBase<Contato>
    {
        Contato ObterContatoPorEmail(string email);
        ICollection<Contato> ObterContatosAtivos();

        void RemoverEndereco(Guid enderecoId);

        Telefone ObterTelefonePorId(Guid telefoneId);
        Telefone AdicionarTelefone(Telefone telefone);
        Telefone AtualizarTelefone(Telefone telefone);
        void RemoverTelefone(Guid telefoneId);

    }
}
