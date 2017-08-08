using System;
using DotNetCoreAppExample.Domain.Usuarios.Entities;
using Moq;
using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using DotNetCoreAppExample.Domain.Usuarios.Services;
using System.Linq;
using Xunit;

namespace DotNetCoreAppExample.Domain.Tests.Usuarios.Services
{
    public class UsuarioDadosServiceTest
    {
        Mock<IUsuarioDadosRepository> _repositoryMock;
        UsuarioDadosService _service;

        public UsuarioDadosServiceTest()
        {
            _repositoryMock = new Mock<IUsuarioDadosRepository>();

            _service = new UsuarioDadosService(_repositoryMock.Object);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Add_SeUsuarioDadosEstaValidoDeveChamarRepository(bool usuarioDadosEstaValido)
        {
            var usuarioDadosMock = new Mock<UsuarioDados>();

            usuarioDadosMock.Setup(x => x.EstaValido()).Returns(usuarioDadosEstaValido);

            _service.Add(usuarioDadosMock.Object);

            usuarioDadosMock.Verify(x => x.EstaValido(), Times.Once);

            if (usuarioDadosEstaValido)
                _repositoryMock.Verify(x => x.Add(usuarioDadosMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.Add(usuarioDadosMock.Object), Times.Never);
        }

        [Fact]
        public void Add_SeCPFJaCadastradoNaoDeveChamarRepository()
        {
            var usuarioDadosMock = new Mock<UsuarioDados>();

            usuarioDadosMock.Setup(x => x.EstaValido()).Returns(true);
            _repositoryMock.Setup(x => x.ObterUsuarioPorCPF(It.IsAny<string>())).Returns(new UsuarioDados(Guid.NewGuid(), "Nome", "12345678901"));

            var retorno = _service.Add(usuarioDadosMock.Object);

            usuarioDadosMock.Verify(x => x.EstaValido(), Times.Once);
            _repositoryMock.Verify(x => x.Add(usuarioDadosMock.Object), Times.Never);
            Assert.True(retorno.ValidationResult.Errors.Any(x => x.ErrorMessage == "O CPF informado já foi cadastrado anteriormente."));
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

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Update_SeUsuarioDadosEstaValidoDeveChamarRepository(bool usuarioDadosEstaValido)
        {
            var usuarioDadosMock = new Mock<UsuarioDados>();

            usuarioDadosMock.Setup(x => x.EstaValido()).Returns(usuarioDadosEstaValido);

            _service.Update(usuarioDadosMock.Object);

            usuarioDadosMock.Verify(x => x.EstaValido(), Times.Once);

            if (usuarioDadosEstaValido)
                _repositoryMock.Verify(x => x.Update(usuarioDadosMock.Object), Times.Once);
            else
                _repositoryMock.Verify(x => x.Update(usuarioDadosMock.Object), Times.Never);
        }
    }
}
