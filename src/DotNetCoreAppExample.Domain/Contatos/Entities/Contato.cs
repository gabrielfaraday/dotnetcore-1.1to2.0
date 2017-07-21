using DotNetCoreAppExample.Domain.Core;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace DotNetCoreAppExample.Domain.Contatos.Entities
{
    public class Contato : EntityBase<Contato>
    {
        public Contato(Guid? id, string nome, string email)
        {
            Id = id ?? Guid.NewGuid();
            Nome = nome;
            Email = email;
            Ativo = true;
        }

        // Construtor para o EF
        protected Contato() { }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        // EF propriedades de navegacao
        public virtual Endereco Endereco { get; private set; }
        public virtual ICollection<Telefone> Telefones { get; set; }
        
        public void AtribuirEndereco(Endereco endereco)
        {
            if (!endereco.EstaValido()) return;
            Endereco = endereco;
        }

        public void AtribuirTelefone(Telefone telefone)
        {
            if (!telefone.EstaValido()) return;
            Telefones.Add(telefone);
        }

        public void RemoverContato()
        {
            Ativo = false;
        }

        public override bool EstaValido()
        {
            ValidarNome();
            ValidarEmail();
            ValidationResult = Validate(this);

            ValidarEndereco();
            ValidarTelefones();

            return ValidationResult.IsValid;
        }

        #region [ Validações ]

        private void ValidarNome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o nome do contato.")
                .Length(2, 150).WithMessage("O nome do contato precisa ter entre 2 e 150 caracteres.");
        }

        private void ValidarEmail()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Informe o e-mail do contato.")
                .MaximumLength(256).WithMessage("O e-mail do contato deve no máximo 256 caracteres.")
                .EmailAddress().WithMessage("E-mail não é válido.");
        }

        private void ValidarEndereco()
        {
            if (Endereco.EstaValido()) return;

            foreach (var erro in Endereco.ValidationResult.Errors)
                ValidationResult.Errors.Add(erro);
        }

        private void ValidarTelefones()
        {
            foreach (var telefone in Telefones)
            {
                if (telefone.EstaValido()) return;

                foreach (var erro in telefone.ValidationResult.Errors)
                    ValidationResult.Errors.Add(erro);
            }
        }

        //private void OutrosExemplosDeValidacao()
        //{
        //    MAIS EXEMPLOS EM: https://github.com/JeremySkinner/FluentValidation
        //
        //    if (!Gratuito)
        //        RuleFor(c => c.Valor)
        //            .ExclusiveBetween(1, 50000)
        //            .WithMessage("O valor deve estar entre 1.00 e 50.000");

        //    if (Gratuito)
        //        RuleFor(c => c.Valor)
        //            .Equal(0).When(e => e.Gratuito)
        //            .WithMessage("O valor não deve diferente de 0 para um evento gratuito");

        //    RuleFor(c => c.DataInicio)
        //        .LessThan(c => c.DataFim)
        //        .WithMessage("A data de início deve ser maior que a data do final do evento");

        //    RuleFor(c => c.DataInicio)
        //        .GreaterThan(DateTime.Now)
        //        .WithMessage("A data de início não deve ser menor que a data atual");

        //    if (Online)
        //        RuleFor(c => c.Endereco)
        //            .Null().When(c => c.Online)
        //            .WithMessage("O evento não deve possuir um endereço se for online");

        //    if (!Online)
        //        RuleFor(c => c.Endereco)
        //            .NotNull().When(c => c.Online == false)
        //            .WithMessage("O evento deve possuir um endereço");
        //}

        #endregion  [ Validações ]
    }
}
