using Application.SQLContext.Usuario.DTOs;
using AutoMapper;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Queries
{
    public class UsuarioSearchAllQueryHandler : IRequestHandler<UsuarioSearchAllQuery, List<UsuarioDTO>>
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly IMapper _mapper;

        public UsuarioSearchAllQueryHandler(IUsuarioServices usuarioServices, IMapper mapper)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
        }
        public async Task<List<UsuarioDTO>> Handle(UsuarioSearchAllQuery request, CancellationToken cancellationToken)
        {
            var entities = await _usuarioServices.GetUsuarios(request.UserId);
            return _mapper.Map<List<UsuarioDTO>>(entities);
        }

    }
}
