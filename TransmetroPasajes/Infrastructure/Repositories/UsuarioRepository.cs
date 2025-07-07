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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PruebaDesarrolladorContext _dbContext;

        public UsuarioRepository(PruebaDesarrolladorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UsuarioGet>> GetUsuarios(int userId)
        {
            try
            {
                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@opc", "LISTAR"),
                    new SqlParameter("@UserId", userId)
                };

                string sql = $"dbo.Sp_Usuarios @opc = @opc, @UserId = @UserId";
                var response = await _dbContext.UsuariosGet.FromSqlRaw(sql, parameters: parameters).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Respuesta>> UpdateUsuarios(Usuario updateUsuario)
        {
            try
            {
                string? encriptarClave = null;

                if (!string.IsNullOrEmpty(updateUsuario.Clave))
                {
                    encriptarClave = encriptarSHA256(updateUsuario.Clave);
                }

                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@opc", "ACTUALIZAR"),
                    new SqlParameter("@UsuarioId", updateUsuario.UsuarioId),
                    new SqlParameter("@Nombre", (object?)updateUsuario.Nombre ?? DBNull.Value),
                    new SqlParameter("@Documento", (object?)updateUsuario.Documento ?? DBNull.Value),
                    new SqlParameter("@Email", (object?)updateUsuario.Email ?? DBNull.Value),
                    new SqlParameter("@Clave", (object?)encriptarClave ?? DBNull.Value)
                };

                string sql = $"dbo.Sp_Usuarios @opc = @opc, @UsuarioId = @UsuarioId, @Nombre = @Nombre, @Documento = @Documento, @Email = @Email, @Clave = @Clave";
                var response = await _dbContext.Respuesta.FromSqlRaw(sql, parameters: parameters).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Respuesta>> DeleteUsuarios(int id)
        {
            try
            {
                SqlParameter[] parameters = new[]
                {
                    new SqlParameter("@opc", "ELIMINAR"),
                    new SqlParameter("@Id", id)
                };

                string sql = $"dbo.Sp_Usuarios @opc = @opc, @Id = @Id";
                var response = await _dbContext.Respuesta.FromSqlRaw(sql, parameters).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error: {ex.Message}");
            }
        }

        #region Private Method
        private static string encriptarSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        #endregion
    }
}
