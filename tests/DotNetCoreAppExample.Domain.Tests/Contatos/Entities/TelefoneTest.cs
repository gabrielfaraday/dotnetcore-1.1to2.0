using DotNetCoreAppExample.Domain.Contatos.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    [TestFixture]
    public class TelefoneTest
    {
        [Test]
        public void Telefone_DDD_DeveTerDoisCaracteres()
        {
            var telefone = new Telefone(Guid.NewGuid(), "333", "33334455", Guid.NewGuid());

            telefone.EstaValido();

            Assert.IsFalse(telefone.ValidationResult.IsValid);
            Assert.IsTrue(telefone.ValidationResult.Errors.Any(e => e.ErrorMessage == "DDD deve ter 2 caracteres"));
        }
    }
}
