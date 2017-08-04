using DotNetCoreAppExample.Domain.Usuarios.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace DotNetCoreAppExample.Domain.Tests.Usuarios.Entities
{
    [TestFixture]
    public class UsuarioDadosTest
    {
        const string NOME_VALIDO = "Nome";
        const string CPF_VALIDO = "71077422725";

        [TestCase(null, "Informe o nome do usuário.")]
        [TestCase("", "Informe o nome do usuário.")]
        [TestCase("a", "O nome do usuário precisa ter entre 2 e 150 caracteres.")]
        [TestCase("ab", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda1", "O nome do usuário precisa ter entre 2 e 150 caracteres.")]
        public void Nome_DeveTerEntre2e150Caracteres(string nome, string mensagemEsperada)
        {
            var usuarioDados = new UsuarioDados(Guid.NewGuid(), nome, CPF_VALIDO);

            usuarioDados.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, usuarioDados);
        }

        [TestCase(null, "Informe o CPF do usuário.")]
        [TestCase("", "Informe o CPF do usuário.")]
        [TestCase("1234567890", "CPF inválido")]
        [TestCase("12345678901", "CPF inválido")]
        [TestCase("54431907920", "")]
        [TestCase("123456789012", "CPF inválido")]
        public void CPF_DeveTer11Caracteres_DeveSerCPFValido(string cpf, string mensagemEsperada)
        {
            var usuarioDados = new UsuarioDados(Guid.NewGuid(), NOME_VALIDO, cpf);

            usuarioDados.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, usuarioDados);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, UsuarioDados usuarioDados)
        {
            Assert.That(usuarioDados.ValidationResult.IsValid, Is.EqualTo(string.IsNullOrEmpty(mensagemEsperada)));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.IsFalse(usuarioDados.ValidationResult.Errors.Any());
            else
                Assert.IsTrue(usuarioDados.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
