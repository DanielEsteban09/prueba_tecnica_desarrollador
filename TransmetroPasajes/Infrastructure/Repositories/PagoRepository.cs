using Core.Entities.SQLContext;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly PruebaDesarrolladorContext _pruebaContext;
        public PagoRepository(PruebaDesarrolladorContext pruebaContext)
        {
            _pruebaContext = pruebaContext;
        }
        public async Task<Respuesta> RegistrarPago(int pasajeId, string? referenciaTransaccion, string? codigoQRBase64)
        {
            var parameters = new[]
            {
                new SqlParameter("@opc", "REGISTRAR_PAGO"),
                new SqlParameter("@PasajeId", pasajeId),
                new SqlParameter("@ReferenciaTransaccion", (object?)referenciaTransaccion ?? DBNull.Value),
                new SqlParameter("@Codigo", (object?)codigoQRBase64 ?? DBNull.Value)
            };

            //var sql = "dbo.Sp_Pago @opc = @opc, @PasajeId = @PasajeId, @ReferenciaTransaccion = @ReferenciaTransaccion, @Codigo = @Codigo";

            //var result = await _pruebaContext.Respuesta.FromSqlRaw(sql, parameters).ToListAsync();

            var sql = @"
              EXEC dbo.Sp_Pago
                @opc                  = @opc,
                @PasajeId             = @PasajeId,
                @ReferenciaTransaccion= @ReferenciaTransaccion,
                @Codigo               = @Codigo";

            var result = await _pruebaContext
                              .Respuesta
                              .FromSqlRaw(sql, parameters)
                              .ToListAsync();


            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Pago>> ObtenerPagoPorIdUsuario(int Id)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@opc", "LISTAR_PAGOS_POR_USUARIO"),
                    new SqlParameter("@UsuarioId", Id)
                };

                var sql = "EXEC dbo.Sp_Pago @opc = @opc, @UsuarioId = @UsuarioId";

                var result = await _pruebaContext.Pagos.FromSqlRaw(sql, parameters).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al listar pasajes: {ex.Message}");
            }
        }
    }
}
