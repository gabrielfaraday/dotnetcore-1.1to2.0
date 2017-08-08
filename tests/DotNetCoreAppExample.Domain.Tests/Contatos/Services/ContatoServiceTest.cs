using System;
using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using Moq;
using DotNetCoreAppExample.Domain.Core.Interfaces;
using DotNetCoreAppExample.Domain.Contatos.Services;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Contatos.Services
{
    public class ContatoServiceTest
    {
        Mock<IContatoRepository> _repositoryMock;
        Mock<IUser> _userMock;

        ContatoService _service;

        public ContatoServiceTest()
        {
            _repositoryMock = new Mock<IContatoRepository>();
            _userMock = new Mock<IUser>();

            _service = new ContatoService(_repositoryMock.Object, _userMock.Object);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Add_DeveAtivarContato_SeContatoEstaValidoDeveChamarRepository(bool contatoEstaValido)
        {
            var contatoMock = new Mock<Contato>();

            contatoMock.Setup(x => x.EstaValido()).Returns(contatoEstaValido);

            _service.Add(contatoMock.Object);

            contatoMock.Verify(x => x.AtivarContato(It.IsAny<Guid>()), Times.Once);
            _userMock.Verify(x => x.GetUserId(), Times.Once);
            contatoMock.Verify(x => x.EstaValido(), Times.Once);

            if (contatoEstaValido)
                _repositoryMock.Verify(x => x.Add(contatoMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.Add(contatoMock.Object), Times.Never);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AdicionarTelefone_SetTelefoneEstaValidoDeveChamarRepository(bool telefoneEstaValido)
        {
            var telefoneMock = new Mock<Telefone>();

            telefoneMock.Setup(x => x.EstaValido()).Returns(telefoneEstaValido);

            _service.AdicionarTelefone(telefoneMock.Object);

            telefoneMock.Verify(x => x.EstaValido(), Times.Once);

            if (telefoneEstaValido)
                _repositoryMock.Verify(x => x.AdicionarTelefone(telefoneMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.AdicionarTelefone(telefoneMock.Object), Times.Never);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void AtualizarTelefone_SeTelefoneEstaValidoDeveChamarRepository(bool telefoneEstaValido)
        {
            var telefoneMock = new Mock<Telefone>();

            telefoneMock.Setup(x => x.EstaValido()).Returns(telefoneEstaValido);

            _service.AtualizarTelefone(telefoneMock.Object);

            telefoneMock.Verify(x => x.EstaValido(), Times.Once);

            if (telefoneEstaValido)
                _repositoryMock.Verify(x => x.AtualizarTelefone(telefoneMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.AtualizarTelefone(telefoneMock.Object), Times.Never);
        }

        [Fact]
        public void Delete_DeveChamarRepository()
        {
            var id = Guid.NewGuid();

            _service.Delete(id);

            _repositoryMock.Verify(x => x.Delete(id), Times.Once);
        }

        [Fact]
        public void Dispose_DeveChamarRepository()
        {
            _service.Dispose();

            _repositoryMock.Verify(x => x.Dispose(), Times.AtLeastOnce);
        }

        [Fact]
        public void FindById_DeveChamarRepository()
        {
            var id = Guid.NewGuid();

            _service.FindById(id);

            _repositoryMock.Verify(x => x.FindById(id), Times.Once);
        }

        [Fact]
        public void GetAll_DeveChamarRepository()
        {
            _service.GetAll();

            _repositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public void ObterAtivos_DeveChamarRepository()
        {
            _service.ObterAtivos();

            _repositoryMock.Verify(x => x.ObterContatosAtivos(), Times.Once);
        }

        [Fact]
        public void ObterPorEmail_DeveChamarRepository()
        {
            var email = "email@com.br";

            _service.ObterPorEmail(email);

            _repositoryMock.Verify(x => x.ObterContatoPorEmail(email), Times.Once);
        }

        [Fact]
        public void ObterTelefonePorId_DeveChamarRepository()
        {
            var id = Guid.NewGuid();

            _service.ObterTelefonePorId(id);

            _repositoryMock.Verify(x => x.ObterTelefonePorId(id), Times.Once);
        }

        [Fact]
        public void RemoverEndereco_DeveChamarRepository()
        {
            var id = Guid.NewGuid();

            _service.RemoverEndereco(id);

            _repositoryMock.Verify(x => x.RemoverEndereco(id), Times.Once);
        }

        [Fact]
        public void RemoverTelefone_DeveChamarRepository()
        {
            var id = Guid.NewGuid();

            _service.RemoverTelefone(id);

            _repositoryMock.Verify(x => x.RemoverTelefone(id), Times.Once);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Update_SeContatoEstaValidoDeveChamarRepository(bool contatoEstaValido)
        {
            var contatoMock = new Mock<Contato>();

            contatoMock.Setup(x => x.EstaValido()).Returns(contatoEstaValido);

            _service.Update(contatoMock.Object);

            contatoMock.Verify(x => x.EstaValido(), Times.Once);

            if (contatoEstaValido)
                _repositoryMock.Verify(x => x.Update(contatoMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.Update(contatoMock.Object), Times.Never);
        }
    }
}
