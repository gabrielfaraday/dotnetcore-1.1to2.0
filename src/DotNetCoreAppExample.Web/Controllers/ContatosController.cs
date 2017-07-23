using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

            return TratarRetorno(retorno, RedirectToAction("Edit", retorno));
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

            return TratarRetorno(retorno, RedirectToAction("Index"));
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

        [Route("adicionar-telefone/{id:guid}")]
        public IActionResult AdicionarTelefone(Guid? id)
        {
            if (id == null)
                return NotFound();

            var contatoViewModel = _contatoAppService.FindById(id.Value);
            return PartialView("_AdicionarTelefone", contatoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar-telefone/{id:guid}")]
        public IActionResult AdicionarTelefone(ContatoViewModel contatoViewModel)
        {
            ModelState.Clear();
            contatoViewModel.TelefoneEmAlteracao.ContatoId = contatoViewModel.Id;
            var retorno = _contatoAppService.AdicionarTelefone(contatoViewModel.TelefoneEmAlteracao);

            if (retorno.ValidationResult.IsValid)
            {
                var url = Url.Action("ObterTelefones", "Contatos", new { id = contatoViewModel.Id });
                return Json(new { success = true, url = url });
            }

            retorno
                .ValidationResult
                .Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.ErrorMessage));

            ViewBag.RetornoPost = "error,Operação não concluida!";

            return PartialView("_AdicionarTelefone", contatoViewModel);
        }

        [Route("alterar-telefone/{id:guid}")]
        public IActionResult AlterarTelefone(Guid? id)
        {
            if (id == null)
                return NotFound();

            var telefoneViewModel = _contatoAppService.ObterTelefonePorId(id.Value);

            if (telefoneViewModel == null)
                NotFound();

            var contatoViewModel = _contatoAppService.FindById(telefoneViewModel.ContatoId);

            contatoViewModel.TelefoneEmAlteracao = telefoneViewModel;

            return PartialView("_AlterarTelefone", contatoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("alterar-telefone/{id:guid}")]
        public IActionResult AlterarTelefone(ContatoViewModel contatoViewModel)
        {
            ModelState.Clear();
            var retorno = _contatoAppService.AtualizarTelefone(contatoViewModel.TelefoneEmAlteracao);

            if (retorno.ValidationResult.IsValid)
            {
                var url = Url.Action("ObterTelefones", "Contatos", new { id = contatoViewModel.TelefoneEmAlteracao.ContatoId });
                return Json(new { success = true, url = url });
            }

            retorno
                .ValidationResult
                .Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.ErrorMessage));

            ViewBag.RetornoPost = "error,Operação não concluida!";

            return PartialView("_AlterarTelefone", contatoViewModel);
        }

        [Route("listar-telefones/{id:guid}")]
        public IActionResult ObterTelefones(Guid id)
        {
            return PartialView("_DetalhesTelefones", _contatoAppService.FindById(id));
        }

        private IActionResult TratarRetorno(ViewModelBase retorno, IActionResult resultado)
        {
            if (retorno.ValidationResult.IsValid)
            {
                ViewBag.RetornoPost = "success,Operação realizada com sucesso!";
                return resultado;
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
