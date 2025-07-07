using Core.Entities.SQLContext;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITarjetaVirtualRepository
    {
        Task<Respuesta> CreateTarjetaVirtual(TarjetaVirtual createTarjetaVirtual);
        Task<IEnumerable<TarjetaVirtual>> GetTarjetaVirtual(int userId);
    }
}
