using DotNetCoreAppExample.Domain.Contatos.Entities;
using System;
using System.Linq;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    public class EnderecoTest
    {
        const string LOGRADOURO_VALIDO = "RUA NOVE";
        const string COMPLEMENTO_VALIDO = "";
        const string NUMERO_VALIDO = "123";
        const string BAIRRO_VALIDO = "CENTRO";
        const string CIDADE_VALIDO = "ARARAQUARA";
        const string CEP_VALIDO = "14810123";
        const string ESTADO_VALIDO = "SP";

        [Theory]
        [InlineData(null, "O Logradouro precisa ser fornecido")]
        [InlineData("", "O Logradouro precisa ser fornecido")]
        [InlineData("a", "O Logradouro precisa ter entre 2 e 150 caracteres")]
        [InlineData("ab", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda1", "O Logradouro precisa ter entre 2 e 150 caracteres")]
        public void Logradouro_DeveTerEntre2e150Caracteres(string logradouro, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), logradouro, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "O Numero precisa ser fornecido")]
        [InlineData("", "O Numero precisa ser fornecido")]
        [InlineData("1", "")]
        [InlineData("123456789a", "")]
        [InlineData("123456789a1", "O Numero precisa ter entre 1 e 10 caracteres")]
        public void Numero_DeveTerEntre1e10Caracteres(string numero, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, numero, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("a", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd1", "O Complemento precisa ter até 100 caracteres")]
        public void Complemento_DeveTerMaximo100Caracteres(string complemento, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, complemento, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "O Bairro precisa ser fornecido")]
        [InlineData("", "O Bairro precisa ser fornecido")]
        [InlineData("a", "O Bairro precisa ter entre 2 e 50 caracteres")]
        [InlineData("ab", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak1", "O Bairro precisa ter entre 2 e 50 caracteres")]
        public void Bairro_DeveTerEntre2e50Caracteres(string bairro, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, bairro, CEP_VALIDO, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "O CEP precisa ser fornecido")]
        [InlineData("", "O CEP precisa ser fornecido")]
        [InlineData("1", "O CEP precisa ter 8 caracteres")]
        [InlineData("12", "O CEP precisa ter 8 caracteres")]
        [InlineData("1234567", "O CEP precisa ter 8 caracteres")]
        [InlineData("12345678", "")]
        [InlineData("123456789", "O CEP precisa ter 8 caracteres")]
        public void CEP_DeveTer8Caracteres(string cep, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, cep, CIDADE_VALIDO, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "A Cidade precisa ser fornecida")]
        [InlineData("", "A Cidade precisa ser fornecida")]
        [InlineData("a", "A Cidade precisa ter entre 2 e 100 caracteres")]
        [InlineData("ab", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd akashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd akashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd ak1", "A Cidade precisa ter entre 2 e 100 caracteres")]
        public void Cidade_DeveTerEntre2e100Caracteres(string cidade, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, cidade, ESTADO_VALIDO);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        [Theory]
        [InlineData(null, "O Estado precisa ser fornecido")]
        [InlineData("", "O Estado precisa ser fornecido")]
        [InlineData("S", "O Estado precisa ter 2 caracteres")]
        [InlineData("SP", "")]
        [InlineData("SPS", "O Estado precisa ter 2 caracteres")]
        public void Estado_DeveTer2Caracteres(string estado, string mensagemEsperada)
        {
            var endereco = new Endereco(Guid.NewGuid(), LOGRADOURO_VALIDO, NUMERO_VALIDO, COMPLEMENTO_VALIDO, BAIRRO_VALIDO, CEP_VALIDO, CIDADE_VALIDO, estado);

            endereco.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, endereco);
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, Endereco endereco)
        {
            Assert.Equal(endereco.ValidationResult.IsValid, string.IsNullOrEmpty(mensagemEsperada));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.Empty(endereco.ValidationResult.Errors);
            else
                Assert.True(endereco.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
