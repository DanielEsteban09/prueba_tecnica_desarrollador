using Application.SQLContext.Usuario.DTOs;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Commands
{
    public class UsuarioUpdateCommand : IRequest<Respuesta>
    {
        [Required]
        public int UsuarioId { get; set; }

        public string? Nombre { get; set; } = null!;

        public string? Documento { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Clave { get; set; }
    }
}
