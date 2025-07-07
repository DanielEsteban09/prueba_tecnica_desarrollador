using Application.SQLContext.TarjetaVirtual.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.Commands
{
    public class TarjetaVirtualCreateCommandHandler : IRequestHandler<TarjetaVirtualCreateCommand, Respuesta>
    {
        private readonly ITarjetaVirtualServices _tarjetaVirtualServices;
        private readonly IMapper _mapper;
        private TarjetaVirtualDTO TarjetaVirtualDTO;
        private Core.Entities.SQLContext.TarjetaVirtual EntityTarjetaVirtual;

        public TarjetaVirtualCreateCommandHandler(ITarjetaVirtualServices tarjetaVirtualServices, IMapper mapper)
        {
            _tarjetaVirtualServices = tarjetaVirtualServices;
            _mapper = mapper;
        }

        public async Task<Respuesta> Handle(TarjetaVirtualCreateCommand request, CancellationToken cancellationToken)
        {
            var tarjetaVirtualDTO = JsonConvert.DeserializeObject<TarjetaVirtualDTO>(request.TarjetaVirtualObject);

            var entityTarjetaVirtual = _mapper.Map<Core.Entities.SQLContext.TarjetaVirtual>(tarjetaVirtualDTO);

            var respuesta = await _tarjetaVirtualServices.CreateTarjetaVirtual(entityTarjetaVirtual);

            if (respuesta == null)
                throw new BusinessException("Error al crear la tarjeta virtual.");

            return respuesta;
        }

    }
}
