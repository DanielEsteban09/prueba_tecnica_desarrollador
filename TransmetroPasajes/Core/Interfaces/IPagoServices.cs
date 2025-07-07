using Core.Entities.SQLContext;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPagoServices
    {
        Task<Respuesta> RegistrarPago(int pasajeId, string? referenciaTransaccion);
        Task<List<Pago>> ObtenerPagoPorIdUsuario(int id);
    }
}
