using AutoMapper;
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
    public class PasajeCreateCommandHandler : IRequestHandler<PasajeCreateCommand, Respuesta>
    {
        private readonly IPasajeServices _pasajeService;
        private readonly IMapper _mapper;

        public PasajeCreateCommandHandler(IPasajeServices estadoService, IMapper mapper)
        {
            _pasajeService = estadoService;
            _mapper = mapper;
        }

        public async Task<Respuesta> Handle(PasajeCreateCommand request, CancellationToken cancellationToken)
        {
            if (request.FechaCompra <= DateTime.MinValue)
            {
                request.FechaCompra = DateTime.Now;
            }

            var entity = new Core.Entities.SQLContext.Pasaje
            {
                UsuarioId = request.UsuarioId,
                TipoPasaje = request.TipoPasaje,
                Precio = request.Precio,
                Cantidad = request.Cantidad,
                FechaCompra = request.FechaCompra,
                Estado = request.Estado,
                Codigo = request.Codigo,
                MedioPago = request.MedioPago,
            };

            // Llamamos al SP y obtenemos la respuesta
            var respuesta = (await _pasajeService.RegistrarPasaje(entity)).FirstOrDefault();

            if (respuesta == null)
            {
                throw new BusinessException($"No se recibió respuesta del procedimiento almacenado");
            }

            return respuesta;
        }
    }
}
