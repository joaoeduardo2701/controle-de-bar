using ControleDeBar.Dominio.ModuloMesa;
using ControleDeBar.Infraestrutura.Compartilhado;
using ControleDeBar.Infraestrutura.ModuloMesa;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Controllers
{
    public class MesaController : Controller
    {
        public ViewResult Listar()
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            List<Mesa> mesas = repositorioMesa.SelecionarTodos();

            ViewBag.Mesas = mesas;

            return View();
        }

        public ViewResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Inserir(Mesa novaMesa)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            repositorioMesa.Inserir(novaMesa);

            HttpContext.Response.StatusCode = 201;

            ViewBag.Mensagem = $"O registro com o ID {novaMesa.Id} foi cadastrado com sucesso!";

            return View("listar");
        }

        public ViewResult Editar(int id)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            Mesa mesa = repositorioMesa.SelecionarPorId(id);

            ViewBag.Mesa = mesa;

            return View("mensagens");
        }

        [HttpPost]
        public ViewResult Editar(int id, Mesa mesaAtualizada)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            Mesa mesaOriginal = repositorioMesa.SelecionarPorId(id);

            mesaAtualizada.Ocupada =
                HttpContext.Request.Form["ocupada"] == "on";

            repositorioMesa.Editar(mesaOriginal, mesaAtualizada);

            ViewBag.Mensagem = $"O registro com o ID {mesaOriginal.Id} foi editado com sucesso!";

            return View("mensagens");
        }

        public ViewResult Excluir(int id)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            Mesa mesa = repositorioMesa.SelecionarPorId(id);

            ViewBag.Mesa = mesa;

            return View();
        }

        [HttpPost, ActionName("excluir")]
        public ViewResult ExcluirConfirmado(int id)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            Mesa mesa = repositorioMesa.SelecionarPorId(id);

            repositorioMesa.Excluir(mesa);

            ViewBag.Mensagem = $"O registro com o ID {mesa.Id} foi excluído com sucesso!";

            return View("mensagens");
        }

        public ViewResult Detalhes(int id)
        {
            ControleDeBarDbContext db = new ControleDeBarDbContext();
            IRepositorioMesa repositorioMesa = new RepositorioMesaEmOrm(db);

            Mesa mesa = repositorioMesa.SelecionarPorId(id);

            ViewBag.Mesa = mesa;

            return View();
        }
    }
}
