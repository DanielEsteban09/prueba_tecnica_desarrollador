using Api.Responses;
using Application.SQLContext.Pasaje.Commands;
using Application.SQLContext.Pasaje.DTOs;
using Application.SQLContext.Pasaje.Queries;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PasajeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PasajeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo para crear un registro de pasaje
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("RegistrarPasaje", Name = "RegistrarPasaje")]
        [Consumes("application/json")]
        public async Task<IActionResult> RegistrarPasaje([FromBody] PasajeCreateCommand entity)
        {
            try
            {
                if (entity is null)
                {
                    throw new ArgumentNullException(nameof(entity), "el objeto 'Pasaje' está vacío");
                }

                var entityResp = await _mediator.Send(entity);
                var response = new ApiResponse<Respuesta>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la creación del registro. Detalle: {e.Message}");
            }
        }

        /// <summary>
        /// Metodo para consultar registros de un usuario especifico
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("ObtenerPasajesPorIdUsuario", Name = "ObtenerPasajesPorIdUsuario")]
        [Consumes("application/json")]
        public async Task<IActionResult> ObtenerPasajesPorIdUsuario([FromQuery] PasajeObtenerTodosByIdQuery entity)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    throw new ArgumentNullException(nameof(entity), "el valor de 'Id' no es válido");
                }

                var entityResp = await _mediator.Send(entity);
                var response = new ApiResponse<IEnumerable<PasajeDTO>>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la búsqueda. Detalle: {e.Message}");
            }
        }

        /// <summary>
        /// Metodo para actualizar un medio de pago de un pasaje especifico
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("ActualizarMedioPago")]
        public async Task<IActionResult> ActualizarMedioPago([FromBody] PasajeUpdateMedioPagoCommand command)
        {
            try
            {
                if (command.PasajeId <= 0 || command.MedioPago <= 0)
                    return BadRequest("Parámetros inválidos");

                var resultado = await _mediator.Send(command);
                return Ok(new ApiResponse<Respuesta>(resultado, 200));
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al actualizar medio de pago. Detalle: {ex.Message}");
            }
        }

    }
}
