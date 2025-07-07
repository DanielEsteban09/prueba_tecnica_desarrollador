using Application.SQLContext.Pasaje.DTOs;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Mapping
{
    public class PasajeMapper : Profile
    {
        public PasajeMapper()
        {
            CreateMap<Core.Entities.SQLContext.Pasaje, PasajeDTO>().ReverseMap();
            CreateMap<Respuesta, PasajeDTO>();
        }
    }
}
