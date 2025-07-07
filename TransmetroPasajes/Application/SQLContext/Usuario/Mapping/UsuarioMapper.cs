using Application.SQLContext.Usuario.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Mapping
{
    internal class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioGet, UsuarioDTO>().ReverseMap();
            CreateMap<Core.Entities.SQLContext.Usuario, UsuarioUpdateDTO>().ReverseMap();
        }
    }
}
