using System.Collections.Generic;
using Aplication.Users.DTO;
using Domain.DTO;
using Domain.Users.Entities;

namespace Aplication.Users
{
    public interface IUsersService
    {
        List<Usuario> GetListUsers();

        LoginDTO GetUsersByEmail(string email, string password);

        UsuarioDTO AddUsuario(UsuarioDTO usuarioDTO);

        bool BorrarUsuario(BorradoDTO usuario);

        bool UpdateUsuario(UsuarioDTO usuarioDTO);

    }


}
