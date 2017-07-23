using System.ComponentModel.DataAnnotations;

namespace DotNetCoreAppExample.Application.ViewModels
{
    public class ViewModelBase
    {
        [ScaffoldColumn(false)]
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }
    }
}
