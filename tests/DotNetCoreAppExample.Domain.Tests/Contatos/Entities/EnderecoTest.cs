using DotNetCoreAppExample.Domain.Contatos.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    [TestFixture]
    public class EnderecoTest
    {
        const string LOGRADOURO_VALIDO = "RUA NOVE";
        const string COMPLEMENTO_VALIDO = "";
        const string NUMERO_VALIDO = "123";
        const string BAIRRO_VALIDO = "CENTRO";
        const string CIDADE_VALIDO = "ARARAQUARA";
        const string CEP_VALIDO = "14810123";
        const string ESTADO_VALIDO = "SP";

        [TestCase(null, "O Logradouro precisa ser fornecido")]
        [TestCase("", "O Logradouro precisa ser fornecido")]
        [TestCase("a", "O Logradouro precisa ter entre 2 e 150 caracteres")]
        [TestCase("ab", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda1", "O Logradouro precisa ter entre 2 e 150 caracteres")]
        public void Logradouro_DeveTerEntre2e150Caracteres(string logradouro, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), logradouro, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "O Numero precisa ser fornecido")]
        [TestCase("", "O Numero precisa ser fornecido")]
        [TestCase("1", "")]
        [TestCase("123456789a", "")]
        [TestCase("123456789a1", "O Numero precisa ter entre 1 e 10 caracteres")]
        public void Numero_DeveTerEntre1e10Caracteres(string numero, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, numero, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "")]
        [TestCase("", "")]
        [TestCase("a", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd1", "O Complemento precisa ter até 100 caracteres")]
        public void Complemento_DeveTerMaximo100Caracteres(string complemento, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, complemento, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "O Bairro precisa ser fornecido")]
        [TestCase("", "O Bairro precisa ser fornecido")]
        [TestCase("a", "O Bairro precisa ter entre 2 e 50 caracteres")]
        [TestCase("ab", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak1", "O Bairro precisa ter entre 2 e 50 caracteres")]
        public void Bairro_DeveTerEntre2e50Caracteres(string bairro, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, bairro, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "O CEP precisa ser fornecido")]
        [TestCase("", "O CEP precisa ser fornecido")]
        [TestCase("1", "O CEP precisa ter 8 caracteres")]
        [TestCase("12", "O CEP precisa ter 8 caracteres")]
        [TestCase("1234567", "O CEP precisa ter 8 caracteres")]
        [TestCase("12345678", "")]
        [TestCase("123456789", "O CEP precisa ter 8 caracteres")]
        public void CEP_DeveTer8Caracteres(string cep, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, cep, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "A Cidade precisa ser fornecida")]
        [TestCase("", "A Cidade precisa ser fornecida")]
        [TestCase("a", "A Cidade precisa ter entre 2 e 100 caracteres")]
        [TestCase("ab", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd akashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak", "")]
        [TestCase("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd akashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak1", "A Cidade precisa ter entre 2 e 100 caracteres")]
        public void Cidade_DeveTerEntre2e100Caracteres(string cidade, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, cidade, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [TestCase(null, "O Estado precisa ser fornecido")]
        [TestCase("", "O Estado precisa ser fornecido")]
        [TestCase("S", "O Estado precisa ter 2 caracteres")]
        [TestCase("SP", "")]
        [TestCase("SPS", "O Estado precisa ter 2 caracteres")]
        public void Estado_DeveTer2Caracteres(string estado, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, estado);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, Endereco endereco)
        {
            Assert.That(endereco.ValidationResult.IsValid, Is.EqualTo(string.IsNullOrEmpty(mensagemEsperada)));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.IsFalse(endereco.ValidationResult.Errors.Any());
            else
                Assert.IsTrue(endereco.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
