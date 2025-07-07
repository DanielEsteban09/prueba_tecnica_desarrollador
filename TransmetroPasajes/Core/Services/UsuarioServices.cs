using Core.Entities;
using Core.Entities.SQLContext;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioServices(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<IEnumerable<UsuarioGet>> GetUsuarios(int userId)
        {
            var getUsuarios = _usuarioRepository.GetUsuarios(userId);
            return getUsuarios;
        }

        public Task<IEnumerable<Respuesta>> UpdateUsuarios(Usuario updateUsuario)
        {
            var RespUpdateUsuario = _usuarioRepository.UpdateUsuarios(updateUsuario);
            return RespUpdateUsuario;
        }

        public Task<IEnumerable<Respuesta>> DeleteUsuarios(int id)
        {
            var deleteUsuario = _usuarioRepository.DeleteUsuarios(id);
            return deleteUsuario;
        }

    }
}
