using Application.SQLContext.Pago.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.Mapping
{
    public class PagoMapper : Profile
    {
        public PagoMapper()
        {
            CreateMap<Core.Entities.SQLContext.Pago, PagoDTO>().ReverseMap();
            CreateMap<Respuesta, PagoDTO>();
        }
    }
}
