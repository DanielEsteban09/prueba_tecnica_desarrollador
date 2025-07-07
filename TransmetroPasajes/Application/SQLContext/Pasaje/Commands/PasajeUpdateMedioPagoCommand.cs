using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Commands
{
    public class PasajeUpdateMedioPagoCommand : IRequest<Respuesta>
    {
        public int PasajeId { get; set; }
        public int MedioPago { get; set; }
    }
}
