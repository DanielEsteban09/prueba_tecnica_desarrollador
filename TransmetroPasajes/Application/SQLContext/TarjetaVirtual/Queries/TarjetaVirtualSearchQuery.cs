using Application.SQLContext.TarjetaVirtual.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.Queries
{
    public record TarjetaVirtualSearchQuery(int UserId) : IRequest<List<TarjetaVirtualDTO>>;
}
