using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAppExample.Application.ViewModels
{
    public class TelefoneViewModel : ViewModelBase
    {
        public TelefoneViewModel()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o DDD do telefone")]
        [StringLength(2, ErrorMessage = "DDD deve ter 2 caracteres")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o número do telefone")]
        [MaxLength(9, ErrorMessage = "Número deve no máximo 9 caracteres")]
        [MinLength(8, ErrorMessage = "Número deve no minimo 8 caracteres")]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [ScaffoldColumn(false)]
        public Guid ContatoId { get; set; }

        public override string ToString()
        {
            return $"({DDD}) {Numero.Substring(0, Numero.Length - 4)}-{Numero.Substring(Numero.Length - 4)}";
        }
    }
}
