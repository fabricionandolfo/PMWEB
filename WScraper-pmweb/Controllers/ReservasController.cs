using HtmlAgilityPack;
using java.sql;
using javax.xml.xpath;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
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
			Reserva reserva = new Reserva();
			reserva.Checkin = checkin;
			reserva.Checkout = checkout;
			reserva.QuantidadeAdulto = quantidadeAdulto;
			reserva.QuantidadeCrianca = quantidadeCrianca;

			Quarto quarto = new Quarto();
			reserva.Quartos = new List<Quarto> { quarto };

			IWebDriver driver = new ChromeDriver();

			// Navegar para a página da web
			driver.Navigate().GoToUrl("https://testautomation.letsbook.com.br/D/Reserva?checkin=02%2F10%2F2023&checkout=03%2F10%2F2023&cidade=&hotel=14&adultos=1&criancas=&destino=Hotel+QA+Absoluto&promocode=&tarifa=&mesCalendario=&_ga=&_gl=&_gcl="); // Substitua pela URL que deseja acessar

			// Aguarde até que a página seja completamente carregada (incluindo o JS)
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));

			// Obtenha o conteúdo HTML da página carregada
			string pageSource = driver.PageSource;

			// Use o HtmlAgilityPack para fazer scraping do conteúdo
			HtmlDocument doc = new HtmlDocument();
			doc.LoadHtml(pageSource);



			

			



			foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes(xpath:"//*[@id=\"tblAcomodacoes\"]/tbody").ToList())
			{
				if (node.Attributes.Count > 0)
				{
					quarto.Nome = node.Descendants().First(x => x.Attributes["class"] != null && x.Attributes["class"].Value.Equals("quartoNome")).InnerText;
				}

			}



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
