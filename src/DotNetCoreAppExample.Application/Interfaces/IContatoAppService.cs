using DotNetCoreAppExample.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Interfaces
{
    public interface IContatoAppService : IAppServiceBase<ContatoViewModel>
    {
        ContatoViewModel ObterPorEmail(string email);
        ICollection<ContatoViewModel> ObterAtivos();

        TelefoneViewModel AdicionarTelefone(TelefoneViewModel telefone);
        TelefoneViewModel AtualizarTelefone(TelefoneViewModel telefone);
        TelefoneViewModel ObterTelefonePorId(Guid telefoneId);
        void RemoverTelefone(Guid telefoneId);
    }
}
