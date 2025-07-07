using Api.Responses;
using Application.SQLContext.Pago.Commands;
using Application.SQLContext.Pago.DTOs;
using Application.SQLContext.Pago.Queries;
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
    public class PagosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PagosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo para registrar un pago y generacion de QR
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("RegistrarPago")]
        public async Task<IActionResult> RegistrarPago([FromBody] PagoCreateCommand command)
        {
            try
            {
                if (command.PasajeId <= 0)
                    return BadRequest("PasajeId inválido");

                var resultado = await _mediator.Send(command);
                return Ok(new ApiResponse<Respuesta>(resultado, 200));
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al registrar el pago. Detalle: {ex.Message}");
            }
        }

        /// <summary>
        /// Metodo para consultar pagos de un usuario especifico
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("ObtenerPagoPorIdUsuario", Name = "ObtenerPagoPorIdUsuario")]
        [Consumes("application/json")]
        public async Task<IActionResult> ObtenerPagoPorIdUsuario([FromQuery] PagoObtenerTodosByIdQuery entity)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    throw new ArgumentNullException(nameof(entity), "el valor de 'Id' no es válido");
                }

                var entityResp = await _mediator.Send(entity);
                var response = new ApiResponse<IEnumerable<PagoDTO>>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la búsqueda. Detalle: {e.Message}");
            }
        }

    }
}
