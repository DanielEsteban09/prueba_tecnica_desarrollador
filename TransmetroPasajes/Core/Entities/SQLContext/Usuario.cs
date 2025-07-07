using System;
using System.Collections.Generic;

namespace Core.Entities.SQLContext;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? Nombre { get; set; } = null!;

    public string? Documento { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public string? Clave { get; set; }

    //public virtual ICollection<Pasaje> Pasajes { get; set; } = new List<Pasaje>();
}
