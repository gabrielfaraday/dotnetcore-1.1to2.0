using DotNetCoreAppExample.Domain.Core;
using FluentValidation;
using System;

namespace DotNetCoreAppExample.Domain.Contatos.Entities
{
    public class Endereco : EntityBase<Endereco>
    {
        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? contatoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            ContatoId = contatoId;
        }

        // Construtor para o EF
        protected Endereco() { }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public Guid? ContatoId { get; private set; }

        // EF propriedades de navegacao
        public virtual Contato Contato { get; private set; }

        public override bool EstaValido()
        {
            ValidarLogradouro();
            ValidarBairro();
            ValidarCEP();
            ValidarCidade();
            ValidarEstado();
            ValidarNumero();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }

        #region  [ Validações ]

        private void ValidarLogradouro()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O Logradouro precisa ser fornecido")
                .Length(2, 150).WithMessage("O Logradouro precisa ter entre 2 e 150 caracteres");
        }

        private void ValidarBairro()
        {
            RuleFor(c => c.Bairro)
                            .NotEmpty().WithMessage("O Bairro precisa ser fornecido")
                            .Length(2, 150).WithMessage("O Bairro precisa ter entre 2 e 150 caracteres");
        }

        private void ValidarCEP()
        {
            RuleFor(c => c.CEP)
                            .NotEmpty().WithMessage("O CEP precisa ser fornecido")
                            .Length(8).WithMessage("O CEP precisa ter 8 caracteres");
        }

        private void ValidarCidade()
        {
            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("A Cidade precisa ser fornecida")
                .Length(2, 150).WithMessage("A Cidade precisa ter entre 2 e 150 caracteres");
        }

        private void ValidarEstado()
        {
            RuleFor(c => c.Estado)
                            .NotEmpty().WithMessage("O Estado precisa ser fornecido")
                            .Length(2, 2).WithMessage("O Estado precisa ter 2 caracteres");
        }

        private void ValidarNumero()
        {
            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O Numero precisa ser fornecido")
                .Length(1, 10).WithMessage("O Numero precisa ter entre 1 e 10 caracteres");
        }

        #endregion  [ Validações ]
    }
}
