using Core.Entities.SQLContext;
using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PagoService : IPagoServices
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }
        public async Task<Respuesta> RegistrarPago(int pasajeId, string? referenciaTransaccion)
        {
            string? codigoQR = null;

            if (!string.IsNullOrEmpty(referenciaTransaccion))
            {
                // Solo generamos QR si es aprobado
                var contenidoQR = $"PasajeId: {pasajeId}\nReferencia: {referenciaTransaccion}\nFecha: {DateTime.Now}";
                codigoQR = QRGeneratorHelper.GenerarQRBase64(contenidoQR);
            }

            var respuesta = await _pagoRepository.RegistrarPago(pasajeId, referenciaTransaccion, codigoQR);

            return respuesta;
        }

        public async Task<List<Pago>> ObtenerPagoPorIdUsuario(int id)
        {
            var pagos = await _pagoRepository.ObtenerPagoPorIdUsuario(id);
            return pagos.ToList();
        }
    }
}
