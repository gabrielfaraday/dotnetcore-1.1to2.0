using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DotNetCoreAppExample.Application.ViewModels;
using DotNetCoreAppExample.Application.Interfaces;

namespace DotNetCoreAppExample.Web.Controllers
{
    [Route("contatos")]
    public class ContatosController : Controller
    {
        readonly IContatoAppService _contatoAppService;

        public ContatosController(IContatoAppService contatoAppService)
        {
            _contatoAppService = contatoAppService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View(_contatoAppService.ObterAtivos());
        }

        [Route("detalhes/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var contatoViewModel = _contatoAppService.FindById(id.Value);

            if (contatoViewModel == null)
                return NotFound();

            return View(contatoViewModel);
        }

        [Route("novo-contato")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("novo-contato")]
        public IActionResult Create(ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
                return View(contatoViewModel);

            var retorno = _contatoAppService.Add(contatoViewModel);

            return TratarRetorno(retorno);
        }

        [Route("alterar-contato/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var contatoViewModel = _contatoAppService.FindById(id.Value);

            if (contatoViewModel == null)
                return NotFound();

            return View(contatoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("alterar-contato/{id:guid}")]
        public IActionResult Edit(ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid) return View(contatoViewModel);

            var retorno = _contatoAppService.Update(contatoViewModel);

            return TratarRetorno(retorno);
        }

        [Route("remover-contato/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var contatoViewModel = _contatoAppService.FindById(id.Value);

            if (contatoViewModel == null)
                return NotFound();

            return View(contatoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("remover-contato/{id:guid}")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _contatoAppService.Delete(id);
            return RedirectToAction("Index");
        }

        private IActionResult TratarRetorno(ContatoViewModel retorno)
        {
            if (retorno.ValidationResult.IsValid)
            {
                ViewBag.RetornoPost = "success,Operação realizada com sucesso!";
                return RedirectToAction("Index");
            }

            retorno
                .ValidationResult
                .Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.ErrorMessage));

            ViewBag.RetornoPost = "error,Operação não concluida!";

            return View(retorno);
        }
    }
}
