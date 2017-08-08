using DotNetCoreAppExample.Domain.Usuarios.Entities;
using System;
using System.Linq;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Usuarios.Entities
{
    public class UsuarioDadosTest
    {
        const string NOME_VALIDO = "Nome";
        const string CPF_VALIDO = "71077422725";

        [Theory]
        [InlineData(null, "Informe o nome do usuário.")]
        [InlineData("", "Informe o nome do usuário.")]
        [InlineData("a", "O nome do usuário precisa ter entre 2 e 150 caracteres.")]
        [InlineData("ab", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda1", "O nome do usuário precisa ter entre 2 e 150 caracteres.")]
        public void Nome_DeveTerEntre2e150Caracteres(string nome, string mensagemEsperada)
        {
            var usuarioDados = new UsuarioDados(Guid.NewGuid(), nome, CPF_VALIDO);

            usuarioDados.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, usuarioDados);
        }

        [Theory]
        [InlineData(null, "Informe o CPF do usuário.")]
        [InlineData("", "Informe o CPF do usuário.")]
        [InlineData("1234567890", "CPF inválido")]
        [InlineData("12345678901", "CPF inválido")]
        [InlineData("54431907920", "")]
        [InlineData("123456789012", "CPF inválido")]
        public void CPF_DeveTer11Caracteres_DeveSerCPFValido(string cpf, string mensagemEsperada)
        {
            var usuarioDados = new UsuarioDados(Guid.NewGuid(), NOME_VALIDO, cpf);

            usuarioDados.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, usuarioDados);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, UsuarioDados usuarioDados)
        {
            Assert.Equal(usuarioDados.ValidationResult.IsValid, string.IsNullOrEmpty(mensagemEsperada));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.Empty(usuarioDados.ValidationResult.Errors);
            else
                Assert.True(usuarioDados.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
