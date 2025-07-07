using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Data;
using Core.Entities.SQLContext;

namespace Api.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;
        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //Encriptamos la contraseña
        public string encriptarSHA256(string texto)
        {
            using(SHA256 sha256Hash = SHA256.Create())
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

        public string generarJWT(Usuario modelo)
        {
            //Crear la informacion del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.UsuarioId.ToString()),
                new Claim(ClaimTypes.Email, modelo.Email!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //Crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

    }
}
