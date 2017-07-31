using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreAppExample.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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
                    data = result ?? viewModel
                });
            }

            var errorMessages = new List<string>();

            viewModel?
                .ValidationResult
                .Errors.ToList()
                .ForEach(e => errorMessages.Add(e.ErrorMessage));

            ModelState
                .Values
                .SelectMany(v => v.Errors).ToList()
                .ForEach(error =>
                {
                    var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                    errorMessages.Add(errorMsg);
                });

            return BadRequest(new
            {
                success = false,
                errors = errorMessages
            });
        }

        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}