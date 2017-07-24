using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAppExample.Application.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DotNetCoreAppExample.Services.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        protected new IActionResult Response(ViewModelBase viewModel = null, object result = null)
        {
            if (ModelState.IsValid && (viewModel == null || viewModel.ValidationResult.IsValid))
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            viewModel?
                .ValidationResult
                .Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.ErrorMessage));

            return BadRequest(new
            {
                success = false,
                errors = ModelState.Values.Select(v => v.Errors)
            });
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}