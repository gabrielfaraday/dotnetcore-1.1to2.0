using DotNetCoreAppExample.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Application.Interfaces
{
    public interface IContatoAppService : IAppServiceBase<ContatoViewModel, NovoContatoViewModel>
    {
        ContatoViewModel ObterPorEmail(string email);
        ICollection<ContatoViewModel> ObterAtivos();

        EnderecoViewModel ObterEnderecoPorId(Guid enderecoId);
        NovoEnderecoViewModel AdicionarEndereco(NovoEnderecoViewModel endereco);
        EnderecoViewModel AtualizarEndereco(EnderecoViewModel endereco);

        NovoTelefoneViewModel AdicionarTelefone(NovoTelefoneViewModel telefone);
        TelefoneViewModel AtualizarTelefone(TelefoneViewModel telefone);
        TelefoneViewModel ObterTelefonePorId(Guid telefoneId);
        void RemoverTelefone(Guid telefoneId);
    }
}
