using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.DTOs
{
    public class TarjetaVirtualDTO
    {
        public int TarjetaVirtualId { get; set; }

        public string Numero { get; set; } = null!;

        public decimal Saldo { get; set; }

        public DateTime? FechaEmision { get; set; }

        public DateTime? FechaUltimaRecarga { get; set; }
        public string? EstadoTarjeta { get; set; }

        public int UsuarioId { get; set; }

        //public virtual Usuario Usuario { get; set; } = null!;
    }
}