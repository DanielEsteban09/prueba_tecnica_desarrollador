using Api.Responses;
using Application.SQLContext.Usuario.Commands;
using Application.SQLContext.Usuario.DTOs;
using Application.SQLContext.Usuario.Queries;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>
        ///Metodo para listar el Usuario registrado
        ///</summary>
        [HttpGet("GetUsuario", Name = "GetUsuarios")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdStr == null)
                    return Unauthorized();

                int userId = int.Parse(userIdStr);

                var entities = await _mediator.Send(new UsuarioSearchAllQuery(userId));
                var response = new ApiResponse<List<UsuarioDTO>>(entities, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la búsqueda. Detalle: {e.Message}");
            }
        }

        /// <summary>
        /// Metodo para editar un usuario
        /// </summary>
        [HttpPut("UpdateUsuario", Name = "UpdateUsuario")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioUpdateCommand entity)
        {
            try
            {
                var entityResp = await _mediator.Send(entity);
                var response = new ApiResponse<Respuesta>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la actualización del usuario. Detalle: {e.Message}");
            }
        }

        /// <summary>
        /// Metodo para eliminar un usuario
        /// </summary>
        [HttpDelete("DeleteUsuario", Name = "DeleteUsuario")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteUsuario([FromQuery] int UsuarioId)
        {
            try
            {
                if (UsuarioId <= 0)
                {
                    throw new ArgumentNullException(nameof(UsuarioId), "el valor de 'Id' no es válido");
                }

                var entityResp = await _mediator.Send(new UsuarioDeleteCommand(UsuarioId));
                var response = new ApiResponse<Respuesta>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la eliminación del registro. Detalle: {e.Message}");
            }
        }

    }
}
