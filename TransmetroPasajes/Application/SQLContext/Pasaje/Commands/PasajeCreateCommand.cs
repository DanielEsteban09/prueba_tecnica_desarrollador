using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Commands
{
    public class PasajeCreateCommand : IRequest<Respuesta>
    {
        //[Required]
        public string? TipoPasaje { get; set; }
        //[Required]
        [Column(TypeName = "numeric(10, 3)")]
        public decimal? Precio { get; set; }
        //[Required]
        public int Cantidad { get; set; }
        //[Required]
        public DateTime FechaCompra { get; set; }
        //[Required]
        public string? Estado { get; set; }
        //[Required]
        public string? Codigo { get; set; }
        //[Required]
        public string? MedioPago { get; set; }
        //[Required]
        public int? UsuarioId { get; set; }
        //[Required]
        //public virtual Usuario? Usuario { get; set; }
    }
}
