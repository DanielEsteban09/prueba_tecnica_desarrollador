using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.Commands
{
    public class PagoCreateCommand : IRequest<Respuesta>
    {
        public int PasajeId { get; set; }
        public string? ReferenciaTransaccion { get; set; }
    }
}
