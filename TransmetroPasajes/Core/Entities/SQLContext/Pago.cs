using System;
using System.Collections.Generic;

namespace Core.Entities.SQLContext;

public partial class Pago
{
    public int PagoId { get; set; }

    public string Medio { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Resultado { get; set; } = null!;

    public int ReferenciaTransaccion { get; set; }

    public int PasajeId { get; set; }

    public virtual Pasaje Pasaje { get; set; } = null!;
}
