using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class UsuarioDTO
    {

        public string Nombre { get; set; } = null!;

        public string Documento { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Clave { get; set; }
    }
}
