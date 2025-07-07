using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Custom;
using Infrastructure.Data;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using Core.Entities.SQLContext;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly PruebaDesarrolladorContext _dbPruebaContext;
        private readonly Utilidades _utilidades;

        public AccesoController(PruebaDesarrolladorContext dbPruebaContext, Utilidades utilidades)
        {
            _dbPruebaContext = dbPruebaContext;
            _utilidades = utilidades;
        }

        ///<summary>
        ///Metodo para registrar a un usuario
        ///</summary>
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            var modeloUser = new Usuario
            {
                Nombre = objeto.Nombre,
                Documento = objeto.Documento,
                Email = objeto.Email,
                Clave = _utilidades.encriptarSHA256(objeto.Clave!)
            };

            await _dbPruebaContext.Usuarios.AddAsync(modeloUser);
            await _dbPruebaContext.SaveChangesAsync();

            if (modeloUser.UsuarioId != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }

        ///<summary>
        ///Metodo para Loguearse
        ///</summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _dbPruebaContext.Usuarios
                                    .Where(u =>
                                        u.Documento == objeto.Documento &&
                                        u.Clave == _utilidades.encriptarSHA256(objeto.Clave)
                                    ).FirstOrDefaultAsync();
            if (usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
        }

    }
}