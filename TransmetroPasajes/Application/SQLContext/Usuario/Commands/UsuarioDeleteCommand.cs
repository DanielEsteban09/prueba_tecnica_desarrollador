using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SQLContext.Usuario.Commands
{
    public class UsuarioDeleteCommand : IRequest<Respuesta>
    {
        [Required]
        public int Id { get; set; }

        public UsuarioDeleteCommand(int id)
        {
            Id = id;
        }
    }
}
