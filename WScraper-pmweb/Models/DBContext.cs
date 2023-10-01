using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WScraper_pmweb.Models
{
	public class DBContext : DbContext
	{
		public DBContext():base("PMWEB")
		{
			Database.CreateIfNotExists();
		}

		public DbSet<Reserva> Reservas { get; set; }
		public DbSet<Quarto> Quartos { get; set; }
	}
}