using Application.SQLContext.TarjetaVirtual.DTOs;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.Commands
{
    public class TarjetaVirtualCreateCommand : IRequest<Respuesta>
    {
        public string TarjetaVirtualObject { get; set; }
    }
}
