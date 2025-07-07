using Core.Entities;
using Core.Entities.SQLContext;
using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TarjetaVirtualRepository : ITarjetaVirtualRepository
    {
        private readonly PruebaDesarrolladorContext _dbContext;

        public TarjetaVirtualRepository(PruebaDesarrolladorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Respuesta> CreateTarjetaVirtual(TarjetaVirtual createTarjetaVirtual)
        {
            SqlParameter[] parameters = new[]
            {
                new SqlParameter("@opc", "CREAR"),
                new SqlParameter("@Numero", createTarjetaVirtual.Numero ?? (object)DBNull.Value),
                new SqlParameter("@Saldo", createTarjetaVirtual.Saldo),
                new SqlParameter("@EstadoTarjeta", createTarjetaVirtual.EstadoTarjeta),
                new SqlParameter("@UsuarioId", createTarjetaVirtual.UsuarioId)
            };

            var sql = "dbo.Sp_TarjetaVirtual @opc = @opc, @Numero = @Numero, @Saldo = @Saldo, @EstadoTarjeta = @EstadoTarjeta, @UsuarioId = @UsuarioId";

            var response = await _dbContext.Respuesta.FromSqlRaw(sql, parameters).ToListAsync();
            return response.FirstOrDefault()!;
        }

        public async Task<IEnumerable<TarjetaVirtual>> GetTarjetaVirtual(int userId)
        {
            try
            {
                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@opc", "LISTAR"),
                    new SqlParameter("@UserId", userId)
                };

                string sql = $"dbo.Sp_TarjetaVirtual @opc = @opc, @UserId = @UserId";
                var response = await _dbContext.TarjetaVirtuals.FromSqlRaw(sql, parameters: parameters).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }
    }
}
