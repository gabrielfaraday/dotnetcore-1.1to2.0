using DotNetCoreAppExample.Domain.Contatos.Entities;
using System;
using System.Linq;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    public class TelefoneTest
    {
        const string DDD_VALIDO = "33";
        const string TELEFONE_VALIDO = "33334455";

        [Theory]
        [InlineData(null, "O DDD precisa ser fornecido")]
        [InlineData("", "O DDD precisa ser fornecido")]
        [InlineData("3", "DDD deve ter 2 caracteres")]
        [InlineData("33", "")]
        [InlineData("333", "DDD deve ter 2 caracteres")]
        public void DDD_DeveTerDoisCaracteres(string ddd, string mensagemEsperada)
        {
            var telefone = new Telefone(Guid.NewGuid(), ddd, TELEFONE_VALIDO, Guid.NewGuid());

            telefone.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, telefone);
        }

        [Theory]
        [InlineData(null, "O Numero precisa ser fornecido")]
        [InlineData("", "O Numero precisa ser fornecido")]
        [InlineData("1234567", "O Numero precisa ter entre 8 e 9 caracteres")]
        [InlineData("12345678", "")]
        [InlineData("123456789", "")]
        [InlineData("1234567890", "O Numero precisa ter entre 8 e 9 caracteres")]
        public void Numero_DeveTerEntreOitoENoveCaracteres(string numero, string mensagemEsperada)
        {
            var telefone = new Telefone(Guid.NewGuid(), DDD_VALIDO, numero, Guid.NewGuid());

            telefone.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, telefone);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, Telefone telefone)
        {
            Assert.Equal(telefone.ValidationResult.IsValid, string.IsNullOrEmpty(mensagemEsperada));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.Empty(telefone.ValidationResult.Errors);
            else
                Assert.True(telefone.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
