using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;


namespace WScraper_pmweb.Models
{
	public class DBContext : DbContext
	{
		public DBContext():base("PMWEB")
		{
			Database.CreateIfNotExists();
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reserva>()
			.HasMany(r => r.Quartos)
			.WithRequired(q => q.Reserva)
			.HasForeignKey(q => q.ReservaId);
		}


		public DbSet<Reserva> Reservas { get; set; }
		public DbSet<Quarto> Quartos { get; set; }

		public List<Reserva> ListarReservasComQuartos()
		{
			return Set<Reserva>()
				.Include(r => r.Quartos) // Inclui os quartos relacionados a cada reserva
				.ToList();
		}
	}
}