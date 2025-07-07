using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Commands
{
    public class PasajeUpdateMedioPagoCommandHandler : IRequestHandler<PasajeUpdateMedioPagoCommand, Respuesta>
    {
        private readonly IPasajeServices _pasajeService;

        public PasajeUpdateMedioPagoCommandHandler(IPasajeServices pasajeService)
        {
            _pasajeService = pasajeService;
        }

        public async Task<Respuesta> Handle(PasajeUpdateMedioPagoCommand request, CancellationToken cancellationToken)
        {
            var resultado = await _pasajeService.ActualizarMedioPago(request.PasajeId, request.MedioPago);

            if (resultado == null)
            {
                throw new BusinessException("No se recibió respuesta del procedimiento almacenado.");
            }

            // Validar si identificador tiene valor antes de usarlo
            /*if (!resultado.identificador.HasValue)
            {
                // Puedes lanzar una excepción o simplemente continuar si el valor es opcional
                throw new BusinessException("El identificador es nulo. Verifica si el PasajeId es correcto.");
            }*/

            return resultado;
        }

    }
}
