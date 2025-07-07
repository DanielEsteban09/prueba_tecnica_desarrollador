using Application.SQLContext.Pago.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pago.Queries
{
    public record PagoObtenerTodosByIdQuery() : IRequest<List<PagoDTO>>
    {
        [Required]
        public int Id { get; set; }
    }
}
