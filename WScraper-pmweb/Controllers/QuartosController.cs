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
    public class QuartosController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Quartos
        public ActionResult Index()
        {
            var quartos = db.Quartos.Include(q => q.Reserva);
            return View(quartos.ToList());
        }

        

        // GET: Quartos/Create
        public ActionResult Create()
        {
            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id");
            return View();
        }

        // POST: Quartos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Condicao,ValorQuato,ValorTaxa,ReservaId")] Quarto quarto)
        {
            if (ModelState.IsValid)
            {
                db.Quartos.Add(quarto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservaId = new SelectList(db.Reservas, "Id", "Id", quarto.ReservaId);
            return View(quarto);
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
