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

namespace Application.SQLContext.Usuario.Commands
{
    public class UsuarioDeleteCommandHandler : IRequestHandler<UsuarioDeleteCommand, Respuesta>
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly IMapper _mapper;

        public UsuarioDeleteCommandHandler(IUsuarioServices usuarioServices, IMapper mapper)
        {
            _usuarioServices = usuarioServices;
            _mapper = mapper;
        }

        public async Task<Respuesta> Handle(UsuarioDeleteCommand request, CancellationToken cancellationToken)
        {
            var resultado = await _usuarioServices.DeleteUsuarios(request.Id);

            var respuesta = resultado.FirstOrDefault();

            if (respuesta == null)
                throw new BusinessException("Ocurrio un erro en el procedimiento almacenado.");

            return respuesta;
        }


    }
}
