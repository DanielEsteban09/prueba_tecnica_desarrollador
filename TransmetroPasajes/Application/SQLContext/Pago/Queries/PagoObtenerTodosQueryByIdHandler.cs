using Application.SQLContext.Pago.DTOs;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.Queries
{
    public class PagoObtenerTodosQueryByIdHandler : IRequestHandler<PagoObtenerTodosByIdQuery, List<PagoDTO>>
    {
        private readonly IPagoServices _pagoService;
        private readonly IMapper _mapper;

        public PagoObtenerTodosQueryByIdHandler(IPagoServices pagoService, IMapper mapper)
        {
            _pagoService = pagoService;
            _mapper = mapper;
        }

        public async Task<List<PagoDTO>> Handle(PagoObtenerTodosByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _pagoService.ObtenerPagoPorIdUsuario(request.Id);
            return _mapper.Map<List<PagoDTO>>(entity);
        }
    }
}
