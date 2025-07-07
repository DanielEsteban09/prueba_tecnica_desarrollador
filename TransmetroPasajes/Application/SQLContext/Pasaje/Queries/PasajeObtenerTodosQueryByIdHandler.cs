using Application.SQLContext.Pasaje.DTOs;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Queries
{
    public class PasajeObtenerTodosQueryByIdHandler : IRequestHandler<PasajeObtenerTodosByIdQuery, List<PasajeDTO>>
    {
        private readonly IPasajeServices _pasajeService;
        private readonly IMapper _mapper;

        public PasajeObtenerTodosQueryByIdHandler(IPasajeServices pasajeService, IMapper mapper)
        {
            _pasajeService = pasajeService;
            _mapper = mapper;
        }

        public async Task<List<PasajeDTO>> Handle(PasajeObtenerTodosByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pasajeService.ObtenerPasajesPorIdUsuario(request.Id);
            return _mapper.Map<List<PasajeDTO>>(entity);
        }
    }
}
