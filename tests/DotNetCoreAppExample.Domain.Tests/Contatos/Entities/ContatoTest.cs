using DotNetCoreAppExample.Domain.Contatos.Entities;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Entities
{
    public class ContatoTest
    {
        const string NOME_VALIDO = "Nome";
        const string EMAIL_VALIDO = "email@prov.com";

        Mock<Endereco> _enderecoMock;
        Mock<Telefone> _telefoneMock;

        public ContatoTest()
        {
            _enderecoMock = new Mock<Endereco>();
            _telefoneMock = new Mock<Telefone>();

            _enderecoMock.Setup(x => x.EstaValido()).Returns(true);
            _telefoneMock.Setup(x => x.EstaValido()).Returns(true);
        }

        [Theory]
        [InlineData(null, "Informe o nome do contato.")]
        [InlineData("", "Informe o nome do contato.")]
        [InlineData("a", "O nome do contato precisa ter entre 2 e 150 caracteres.")]
        [InlineData("ab", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda", "")]
        [InlineData("ashdgaskhdgas jagsdjgas dgajsgd kasgd gsad asgd aksgd kasgdkjasgdj gasjkdg ajskgd kasgd aksgd aksjgd akjsgd askgd askgd askdg aksdg askdg aksdg askgda1", "O nome do contato precisa ter entre 2 e 150 caracteres.")]
        public void Nome_DeveTerEntre2e150Caracteres(string nome, string mensagemEsperada)
        {
            var contato = new Contato(Guid.NewGuid(), nome, EMAIL_VALIDO, null);

            contato.AtribuirEndereco(_enderecoMock.Object);
            contato.AtribuirTelefone(_telefoneMock.Object);

            contato.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, contato);
        }

        [Theory]
        [InlineData(null, "Informe o e-mail do contato.")]
        [InlineData("", "Informe o e-mail do contato.")]
        [InlineData("a", "E-mail não é válido.")]
        [InlineData("ab@", "E-mail não é válido.")]
        [InlineData("ab@c", "E-mail não é válido.")]
        [InlineData("ab@c.com", "")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@teste.com", "")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@teste.com", "O e-mail do contato deve no máximo 256 caracteres.")]
        public void Email_DeveTerEntre1e256CaracteresEDeveSerEmailValido(string email, string mensagemEsperada)
        {
            var contato = new Contato(Guid.NewGuid(), NOME_VALIDO, email, null);

            contato.AtribuirEndereco(_enderecoMock.Object);
            contato.AtribuirTelefone(_telefoneMock.Object);

            contato.EstaValido();

            AssertMensagemEsperada(mensagemEsperada, contato);
        }

        [Fact]
        public void Contato_DeveValidarEnderecoETelefonesSeHouver()
        {
            var contato = new Contato(Guid.NewGuid(), NOME_VALIDO, EMAIL_VALIDO, null);

            contato.AtribuirEndereco(_enderecoMock.Object);
            contato.AtribuirTelefone(_telefoneMock.Object);

            contato.EstaValido();

            _enderecoMock.Verify(x => x.EstaValido(), Times.Exactly(2));
            _telefoneMock.Verify(x => x.EstaValido(), Times.Exactly(2));
        }

        private static void AssertMensagemEsperada(string mensagemEsperada, Contato contato)
        {
            Assert.Equal(contato.ValidationResult.IsValid, string.IsNullOrEmpty(mensagemEsperada));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.Empty(contato.ValidationResult.Errors);
            else
                Assert.True(contato.ValidationResult.Errors.Any(e => e.ErrorMessage == mensagemEsperada));
        }
    }
}
