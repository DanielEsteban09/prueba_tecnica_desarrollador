using System;
using System.Collections.Generic;

namespace Core.Entities.SQLContext;

public partial class TarjetaVirtual
{
    public int TarjetaVirtualId { get; set; }

    public string Numero { get; set; } = null!;

    public decimal Saldo { get; set; }

    public DateTime? FechaEmision { get; set; }

    public DateTime? FechaUltimaRecarga { get; set; }
    public string? EstadoTarjeta { get; set; }

    public int UsuarioId { get; set; }

    //public virtual Usuario Usuario { get; set; } = null!;
}