using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WScraper_pmweb.Models
{
	[Table("Reservas")]
	public class Reserva
	{
		public int Id { get; set; }

		#region DataReserva
		[Required]
		[Display(Name = "Data do Checkin")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
		public DateTime Checkin { get; set; }
		[Required]
		[Display(Name = "Data do Checkout")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = true)]
		public DateTime Checkout { get; set; }
        #endregion

        public int QuantidadeAdulto { get; set; }	
		public int QuantidadeCrianca { get; set; }

		public virtual ICollection<Quarto> Quartos { get; set; }
	}
}