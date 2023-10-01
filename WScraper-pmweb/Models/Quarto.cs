using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WScraper_pmweb.Models
{
	[Table("Quartos")]
	public class Quarto
	{
		#region InforQuerto
		public int Id { get; set; }
		[Required]
		public string Nome { get; set; }
		[Required]
		public string Condicao { get; set; }
		[Required]
		public float ValorQuato { get; set; }
		[Required]
		public float ValorTaxa { get; set; }
		#endregion
	}
}