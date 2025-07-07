using Core.Entities;
using Core.Entities.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUsuarioServices
    {
        Task<IEnumerable<UsuarioGet>> GetUsuarios(int userId);
        Task<IEnumerable<Respuesta>> UpdateUsuarios(Usuario updateUsuario);
        Task<IEnumerable<Respuesta>> DeleteUsuarios(int id);
    }
}
