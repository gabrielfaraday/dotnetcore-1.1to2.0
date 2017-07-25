using DotNetCoreAppExample.Domain.Usuarios.Interfaces;
using DotNetCoreAppExample.Domain.Usuarios.Entities;
using DotNetCoreAppExample.Domain.Core;
using FluentValidation.Results;

namespace DotNetCoreAppExample.Domain.Usuarios.Services
{
    public class UsuarioDadosService : ServiceBase<UsuarioDados, IUsuarioDadosRepository>, IUsuarioDadosService
    {
        public UsuarioDadosService(IUsuarioDadosRepository repository) : base(repository)
        {
        }

        public override UsuarioDados Add(UsuarioDados entity)
        {
            entity.EstaValido();

            if (_repository.ObterUsuarioPorCPF(entity.CPF) != null)
            {
                entity
                    .ValidationResult.Errors
                    .Add(new ValidationFailure("CPF", "O CPF informado já foi cadastrado anteriormente."));
            }

            if (!entity.ValidationResult.IsValid)
                return entity;

            return _repository.Add(entity);
        }
    }
}