using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAppExample.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 100 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "As senhas informadas estão diferentes.")]
        public string ConfirmPassword { get; set; }
    }
}
