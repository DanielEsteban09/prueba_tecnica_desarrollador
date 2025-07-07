using Core.Entities.SQLContext;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PasajeService :IPasajeServices
    {
        private readonly IPasajeRepository _pasajeRepository;

        public PasajeService(IPasajeRepository pasajeRepository)
        {
            _pasajeRepository = pasajeRepository;
        }

        public async Task<IEnumerable<Respuesta>> RegistrarPasaje(Pasaje pasaje)
        {
            var registroPasaje = await _pasajeRepository.RegistrarPasaje(pasaje);
            return registroPasaje;
        }

        public async Task<List<Pasaje>> ObtenerPasajesPorIdUsuario(int id)
        {
            var pasajes = await _pasajeRepository.ObtenerPasajesPorIdUsuario(id);
            return pasajes.ToList();
        }

        public async Task<Respuesta> ActualizarMedioPago(int pasajeId, int medioPago)
        {
            return await _pasajeRepository.ActualizarMedioPago(pasajeId, medioPago);
        }
    }
}
