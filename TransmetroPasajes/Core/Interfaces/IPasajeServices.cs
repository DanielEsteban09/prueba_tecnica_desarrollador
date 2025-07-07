using Core.Entities.SQLContext;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPasajeServices
    {
        Task<IEnumerable<Respuesta>> RegistrarPasaje(Pasaje pasaje);
        Task<List<Pasaje>> ObtenerPasajesPorIdUsuario(int id);
        Task<Respuesta> ActualizarMedioPago(int pasajeId, int medioPago);
    }
}
