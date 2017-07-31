using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAppExample.Application.ViewModels
{
    public class ContatoViewModel : ViewModelBase
    {
        public ContatoViewModel()
        {
            Id = Guid.NewGuid();
            Telefones = new List<TelefoneViewModel>();
            Endereco = new EnderecoViewModel();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do contato")]
        [MaxLength(150, ErrorMessage = "O tamanho maximo do nome é 150 caracteres")]
        [MinLength(2, ErrorMessage = "O tamanho minimo do nome é 2 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail do contato")]
        [MaxLength(256, ErrorMessage = "O tamanho maximo do e-mail é 256 caracteres")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [ScaffoldColumn(false)]
        public bool Ativo { get; set; }

        [ScaffoldColumn(false)]
        public Guid AtivadoPor { get; set; }

        [DisplayName("Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [ScaffoldColumn(false)]
        public Guid EnderecoId { get; set; }

        public ICollection<TelefoneViewModel> Telefones { get; set; }

        public EnderecoViewModel Endereco { get; set; }

        public TelefoneViewModel TelefoneEmAlteracao { get; set; }
    }
}
