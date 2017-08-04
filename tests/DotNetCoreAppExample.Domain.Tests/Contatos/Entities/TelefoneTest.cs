using DotNetCoreAppExample.Domain.Contatos.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    [TestFixture]
    public class TelefoneTest
    {
        const string DDD_VALIDO = "33";
        const string TELEFONE_VALIDO = "33334455";

        [TestCase(null, "O DDD precisa ser fornecido")]
        [TestCase("", "O DDD precisa ser fornecido")]
        [TestCase("3", "DDD deve ter 2 caracteres")]
        [TestCase("33", "")]
        [TestCase("333", "DDD deve ter 2 caracteres")]
        public void DDD_DeveTerDoisCaracteres(string ddd, string mensagemEsperada)
        {
            var telefone = new Telefone(Guid.NewGuid(), ddd, TELEFONE_VALIDO, Guid.NewGuid());

            telefone.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, telefone);
        }

        [TestCase(null, "O Numero precisa ser fornecido")]
        [TestCase("", "O Numero precisa ser fornecido")]
        [TestCase("1234567", "O Numero precisa ter entre 8 e 9 caracteres")]
        [TestCase("12345678", "")]
        [TestCase("123456789", "")]
        [TestCase("1234567890", "O Numero precisa ter entre 8 e 9 caracteres")]
        public void Numero_DeveTerEntreOitoENoveCaracteres(string numero, string mensagemEsperada)
        {
            var telefone = new Telefone(Guid.NewGuid(), DDD_VALIDO, numero, Guid.NewGuid());

            telefone.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, telefone);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, Telefone telefone)
        {
            Assert.That(telefone.ValidationResult.IsValid, Is.EqualTo(string.IsNullOrEmpty(mensagemEsperada)));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.IsFalse(telefone.ValidationResult.Errors.Any());
            else
                Assert.IsTrue(telefone.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
