using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.DTOs
{
    public class PagoDTO
    {
        public int PagoId { get; set; }
        public string Medio { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Resultado { get; set; } = null!;
        public int ReferenciaTransaccion { get; set; }
        public int PasajeId { get; set; }
    }
}
