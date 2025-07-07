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
using Azure.Core;
using System.Data;

namespace Infrastructure.Repositories
{
    public class PasajeRepository : IPasajeRepository
    {
        private readonly PruebaDesarrolladorContext _pruebaContext;

        public PasajeRepository(PruebaDesarrolladorContext pruebaContext)
        {
            _pruebaContext = pruebaContext;
        }

        public async Task<IEnumerable<Respuesta>> RegistrarPasaje(Pasaje pasaje)
        {
            try
            {
                var parameters = new[]
{
                new SqlParameter("@opc", SqlDbType.Char, 20)
                    { Value = "CREAR" },
                new SqlParameter("@UsuarioId", SqlDbType.Int)
                    { Value = pasaje.UsuarioId ?? (object)DBNull.Value },
                new SqlParameter("@TipoPasaje", SqlDbType.Int)
                    { Value = int.Parse(pasaje.TipoPasaje!) },
                new SqlParameter("@Cantidad", SqlDbType.Int)
                    { Value = pasaje.Cantidad },
                new SqlParameter("@FechaCompra", SqlDbType.DateTime2)
                    { Value = pasaje.FechaCompra },
                new SqlParameter("@Codigo", SqlDbType.NVarChar, -1)
                    { Value = pasaje.Codigo ?? (object)DBNull.Value },
                new SqlParameter("@MedioPago", SqlDbType.Int)
                    { Value = int.Parse(pasaje.MedioPago!) }
            };

                string sql = @"
                  EXEC dbo.Sp_Pasaje 
                    @opc        = @opc,
                    @UsuarioId  = @UsuarioId,
                    @TipoPasaje = @TipoPasaje,
                    @Cantidad   = @Cantidad,
                    @FechaCompra= @FechaCompra,
                    @Codigo     = @Codigo,
                    @MedioPago  = @MedioPago";
                                var response = await _pruebaContext
                                                    .Respuesta
                                                    .FromSqlRaw(sql, parameters)
                                                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Pasaje>> ObtenerPasajesPorIdUsuario(int Id)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@opc", "LISTAR_POR_USUARIO"),
                    new SqlParameter("@UsuarioId", Id)
                };

                var sql = "EXEC dbo.Sp_Pasaje @opc = @opc, @UsuarioId = @UsuarioId";

                var result = await _pruebaContext.Pasajes.FromSqlRaw(sql, parameters).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al listar pasajes: {ex.Message}");
            }
        }

        public async Task<Respuesta> ActualizarMedioPago(int pasajeId, int medioPago)
        {
            var parameters = new[]
            {
                new SqlParameter("@opc", "ACTUALIZAR_MEDIO_PAGO"),
                new SqlParameter("@PasajeId", pasajeId),
                new SqlParameter("@MedioPago", medioPago)
            };

            var sql = "dbo.Sp_Pasaje @opc = @opc, @PasajeId = @PasajeId, @MedioPago = @MedioPago";

            var result = await _pruebaContext.Respuesta.FromSqlRaw(sql, parameters).ToListAsync();

            var respuesta = result.FirstOrDefault();

            if (respuesta == null)

                throw new BusinessException("No se obtuvo respuesta desde el procedimiento.");

            return respuesta;
        }


    }
}
