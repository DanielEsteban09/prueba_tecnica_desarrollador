using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.Commands
{
    public class PagoCreateCommandHandler : IRequestHandler<PagoCreateCommand, Respuesta>
    {
        private readonly IPagoServices _pagoService;

        public PagoCreateCommandHandler(IPagoServices pagoService)
        {
            _pagoService = pagoService;
        }

        public async Task<Respuesta> Handle(PagoCreateCommand request, CancellationToken cancellationToken)
        {
            var respuesta = await _pagoService.RegistrarPago(request.PasajeId, request.ReferenciaTransaccion);

            if (respuesta == null)
                throw new BusinessException("No se recibió respuesta del procedimiento almacenado.");

            return respuesta;
        }
    }
}
