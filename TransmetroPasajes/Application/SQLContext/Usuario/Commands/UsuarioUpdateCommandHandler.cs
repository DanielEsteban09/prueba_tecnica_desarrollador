using Application.SQLContext.Usuario.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Commands
{
    public class UsuarioUpdateCommandHandler : IRequestHandler<UsuarioUpdateCommand, Respuesta>
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly IMapper _mapper;

        public UsuarioUpdateCommandHandler(IUsuarioServices usuarioServices, IMapper mapper)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
        }

        public async Task<Respuesta> Handle(UsuarioUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = new Core.Entities.SQLContext.Usuario
            {
                UsuarioId = request.UsuarioId,
                Nombre = request.Nombre,
                Documento = request.Documento,
                Email = request.Email,
                Clave = request.Clave
            };

            var resultado = await _usuarioServices.UpdateUsuarios(entity);

            return resultado.FirstOrDefault()!;
        }


    }
}
