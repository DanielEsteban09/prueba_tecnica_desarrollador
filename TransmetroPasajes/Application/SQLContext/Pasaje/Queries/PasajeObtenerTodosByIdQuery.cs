using Application.SQLContext.Pasaje.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Pasaje.Queries
{
    public record PasajeObtenerTodosByIdQuery() : IRequest<List<PasajeDTO>>
    {
        [Required]
        public int Id { get; set; }
    }
}
