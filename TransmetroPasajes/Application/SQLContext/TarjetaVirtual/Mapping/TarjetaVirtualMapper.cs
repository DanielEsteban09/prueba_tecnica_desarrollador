using Application.SQLContext.TarjetaVirtual.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.TarjetaVirtual.Mapping
{
    public class TarjetaVirtualMapper : Profile
    {
        public TarjetaVirtualMapper()
        {
            CreateMap<Core.Entities.SQLContext.TarjetaVirtual, TarjetaVirtualDTO>().ReverseMap();
        }
    }
}
