using Application.SQLContext.TarjetaVirtual.DTOs;
using Application.SQLContext.Usuario.Queries;
using AutoMapper;
using Core.Interfaces;
using Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.Queries
{
    public class TarjetaVirtualSearchQueryHandler : IRequestHandler<TarjetaVirtualSearchQuery, List<TarjetaVirtualDTO>>
    {
        private readonly ITarjetaVirtualServices _tarjetaVirtualServices;
        private readonly IMapper _mapper;

        public TarjetaVirtualSearchQueryHandler(ITarjetaVirtualServices tarjetaVirtualServices, IMapper mapper)
        {
            _tarjetaVirtualServices = tarjetaVirtualServices;
            _mapper = mapper;
        }

        public async Task<List<TarjetaVirtualDTO>> Handle(TarjetaVirtualSearchQuery request, CancellationToken cancellationToken)
        {
            var entities = await _tarjetaVirtualServices.GetTarjetaVirtual(request.UserId);
            return _mapper.Map<List<TarjetaVirtualDTO>>(entities);
        }
    }
}
