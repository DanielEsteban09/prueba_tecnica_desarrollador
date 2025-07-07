using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }

        public string Nombre { get; set; } = null!;

        public string Documento { get; set; } = null!;

        public string Email { get; set; } = null!;

        //public string? Clave { get; set; }
    }
}
