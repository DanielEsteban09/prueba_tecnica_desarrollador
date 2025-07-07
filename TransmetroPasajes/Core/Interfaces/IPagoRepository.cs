using Core.Entities.SQLContext;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPagoRepository
    {
        Task<Respuesta> RegistrarPago(int pasajeId, string? referenciaTransaccion, string? codigoQRBase64);
        Task<IEnumerable<Pago>> ObtenerPagoPorIdUsuario(int Id);
    }
}
