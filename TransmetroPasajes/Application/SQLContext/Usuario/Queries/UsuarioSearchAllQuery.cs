using Application.SQLContext.Usuario.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Queries
{
    public record UsuarioSearchAllQuery(int UserId) : IRequest<List<UsuarioDTO>>;
}
