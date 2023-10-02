using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WScraper_pmweb.Models;

namespace WScraper_pmweb.Controllers
{
    public class ReservasController : Controller
    {
        private DBContext db = new DBContext();

		// GET: Reservas
		public ActionResult Index(DateTime? checkin = null, DateTime? checkout = null, int? quantidadeAdulto = null, int? quantidadeCrianca = null)
		{
			if (checkin != null && checkout != null && quantidadeAdulto != null && quantidadeCrianca != null)
			{
				if (buscaReservas(checkin.Value, checkout.Value, quantidadeAdulto.Value, quantidadeCrianca.Value).Count != 0)
                    return View(buscaReservas(checkin.Value, checkout.Value, quantidadeAdulto.Value, quantidadeCrianca.Value));

				CriarCache(checkin.Value, checkout.Value, quantidadeAdulto.Value, quantidadeCrianca.Value);

					return View(buscaReservas(checkin.Value, checkout.Value, quantidadeAdulto.Value, quantidadeCrianca.Value));
			}

			return View(db.ListarReservasComQuartos().ToList());
		}

        // GET: Reservas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reservas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Checkin,Checkout,QuantidadeAdulto,QuantidadeCrianca")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {

				Quarto quarto = new Quarto();
				quarto.Nome = "fabricio";
				quarto.Condicao = "teste sc";
				quarto.ValorQuato = 500;
				quarto.ValorTaxa = 50;

				reserva.Quartos = new List<Quarto> { quarto };

				db.Reservas.Add(reserva);
				db.SaveChanges();
                return RedirectToAction("Index");
            }

			return View(reserva);
        }

        public void CriarCache(DateTime checkin, DateTime checkout, int quantidadeAdulto, int quantidadeCrianca) 
        {
        
        }
        public List<Reserva> buscaReservas(DateTime checkin, DateTime checkout, int quantidadeAdulto, int quantidadeCrianca) 
        {
			var ListaReservaQuartos = db.ListarReservasComQuartos().Where(a => a.Checkin == checkin && a.Checkout == checkout && a.QuantidadeAdulto == quantidadeAdulto && a.QuantidadeCrianca == quantidadeCrianca).ToList();
            return ListaReservaQuartos;
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
