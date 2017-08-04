using System;
using DotNetCoreAppExample.Domain.Core;
using FluentValidation;
using DotNetCoreAppExample.Domain._Validations;

namespace DotNetCoreAppExample.Domain.Usuarios.Entities
{
    public class UsuarioDados : EntityBase<UsuarioDados>
    {
        public UsuarioDados(Guid id, string nome, string cpf)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
        }

        // Construtor para o EF
        protected UsuarioDados() { }

        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public override bool EstaValido()
        {
            ValidarNome();
            ValidarCPF();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        #region [ Validações ]

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome do usuário.")
                .Length(2, 150).WithMessage("O nome do usuário precisa ter entre 2 e 150 caracteres.");
        }

        private void ValidarCPF()
        {
            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("Informe o CPF do usuário.")
                .Must(CPFValidation.CPFValido).WithMessage("CPF inválido");
        }

        #endregion [ Validações ]
    }
}
