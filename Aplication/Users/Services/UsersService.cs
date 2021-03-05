using Aplication.Users.DTO;
using Domain.DTO;
using Domain.Users.Entities;
using Infrastructure.Users.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplication.Users.Services
{
    public class UsersService : IUsersService
    {

        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public UsuarioDTO AddUsuario(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public bool BorrarUsuario(BorradoDTO usuario)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetListUsers()
        {
            try
            {


                List<Usuario> pRet = (from a in usersRepository.GetListUsers()

                                         select new Usuario()
                                         {
                                             Nombre   = a.Nombre,
                                             Apellido = a.Apellido,
                                             Email    = a.Email,
                                             Movil    = a.Movil

                                         }).ToList();

                return pRet;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public LoginDTO GetUsersByEmail(string email,string password)
        {
            try
            {

                LoginDTO pRet = usersRepository.GetUsersByEmail(email,password);

                return pRet;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
