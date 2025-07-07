using Core.Entities;
using Core.Entities.SQLContext;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TarjetaVirtualServices : ITarjetaVirtualServices
    {
        private readonly ITarjetaVirtualRepository _tarjetaVirtualRepository;

        public TarjetaVirtualServices(ITarjetaVirtualRepository tarjetaVirtualRepository)
        {
            _tarjetaVirtualRepository = tarjetaVirtualRepository;
        }

        public Task<Respuesta> CreateTarjetaVirtual(TarjetaVirtual createTarjetaVirtual)
        {
            var RespCreateTarjetaVirtual = _tarjetaVirtualRepository.CreateTarjetaVirtual(createTarjetaVirtual);
            return RespCreateTarjetaVirtual;
        }

        public Task<IEnumerable<TarjetaVirtual>> GetTarjetaVirtual(int userId)
        {
            var getTarjetaVirtual = _tarjetaVirtualRepository.GetTarjetaVirtual(userId);
            return getTarjetaVirtual;
        }
    }
}
