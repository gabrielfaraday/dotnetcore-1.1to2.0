using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAppExample.Application.ViewModels
{
    public class NovoEnderecoViewModel : EnderecoViewModel
    {
    }

    public class EnderecoViewModel
    {
        public EnderecoViewModel()
        {
            Id = Guid.NewGuid();
        }

        public SelectList Estados()
        {
            return new SelectList(EstadoViewModel.ListarEstados(), "UF", "Nome");
        }

        [Key]
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "O logradouro deve ser informado")]
        //[MinLength(2, ErrorMessage = "O tamanho minimo do logradouro é 2 caracteres")]
        //[MaxLength(150, ErrorMessage = "O tamanho máximo do logradouro é 150 caracteres")]
        public string Logradouro { get; set; }

        //[Required(ErrorMessage = "O numero deve ser informado")]
        //[MinLength(1, ErrorMessage = "O tamanho minimo do numero é 1 caracteres")]
        //[MaxLength(10, ErrorMessage = "O tamanho máximo do numero é 10 caracteres")]
        [DisplayName("Número")]
        public string Numero { get; set; }

        //[MaxLength(100, ErrorMessage = "O tamanho máximo do complemento é 100 caracteres")]
        public string Complemento { get; set; }

        //[Required(ErrorMessage = "O bairro deve ser informado")]
        //[MinLength(2, ErrorMessage = "O tamanho minimo do bairro é 2 caracteres")]
        //[MaxLength(50, ErrorMessage = "O tamanho máximo do bairro é 50 caracteres")]
        public string Bairro { get; set; }

        //[Required(ErrorMessage = "O CEP deve ser informado")]
        //[MaxLength(8, ErrorMessage = "O tamanho máximo do CEP é 8 caracteres")]
        public string CEP { get; set; }

        //[Required(ErrorMessage = "A cidade deve ser informado")]
        //[MinLength(2, ErrorMessage = "O tamanho minimo da cidade é 2 caracteres")]
        //[MaxLength(100, ErrorMessage = "O tamanho máximo da cidade é 100 caracteres")]
        public string Cidade { get; set; }

        //[Required(ErrorMessage = "A cidade deve ser informado")]
        //[StringLength(2, MinimumLength = 2, ErrorMessage = "O tamanho de estado é 2 caracteres")]
        public string Estado { get; set; }

        [ScaffoldColumn(false)]
        public Guid ContatoId { get; set; }

        [ScaffoldColumn(false)]
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

        public override string ToString()
        {
            return $"{Logradouro} , {Numero} - {Complemento} - {Bairro}, {Cidade}/{Estado}";
        }
    }
}