using Api.Responses;
using Application.SQLContext.TarjetaVirtual.Commands;
using Application.SQLContext.TarjetaVirtual.DTOs;
using Application.SQLContext.TarjetaVirtual.Queries;
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
    public class TarjetaVirtualController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TarjetaVirtualController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo para crear una tarjeta virtual
        /// </summary>
        [HttpPost("CreateTarjetaVirtual", Name = "CreateTarjetaVirtual")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTarjetaVirtual([FromForm] TarjetaVirtualCreateCommand entity)
        {
            try
            {
                var entityResp = await _mediator.Send(entity);

                var response = new ApiResponse<Respuesta>(entityResp, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la creación del registro. Detalle: {e.Message}");
            }
        }

        ///<summary>
        ///Metodo para listar la tarjeta virtual
        ///</summary>
        [HttpGet("GetTarjetaVirtual/{userId}", Name = "GetTarjetaVirtual")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetTarjetaVirtual(int userId)
        {
            try
            {
                var entities = await _mediator.Send(new TarjetaVirtualSearchQuery(userId));
                var response = new ApiResponse<List<TarjetaVirtualDTO>>(entities, 200);
                return Ok(response);
            }
            catch (Exception e)
            {
                throw new BusinessException($"Error en la búsqueda. Detalle: {e.Message}");
            }
        }

    }
}
