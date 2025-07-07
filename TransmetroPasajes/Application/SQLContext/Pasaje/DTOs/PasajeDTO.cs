using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.DTOs
{
    public class PasajeDTO
    {
        public int PasajeId { get; set; }
        public string TipoPasaje { get; set; } = null!;

        [Column(TypeName = "numeric(10, 3)")]
        public decimal? Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;
        public string? Estado { get; set; }
        public string? Codigo { get; set; }
        public string? MedioPago { get; set; }
        public int? UsuarioId { get; set; }

        //public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

        //public virtual Usuario? Usuario { get; set; }
    }
}
