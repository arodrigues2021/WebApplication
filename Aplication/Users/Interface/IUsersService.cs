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

        int AddUsuario(UsuarioDTO usuarioDTO);

        int BorrarUsuario(BorradoDTO usuario);

        int UpdateUsuario(UsuarioDTO usuarioDTO);

    }


}
