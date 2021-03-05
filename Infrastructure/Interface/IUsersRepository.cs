using Domain.Users.Entities;
using System.Collections.Generic;
using Aplication.Users.DTO;

namespace Infrastructure.Users.Interface
{
    public interface IUsersRepository
    {
        List<Usuario> GetListUsers();
        LoginDTO GetUsersByEmail(string email);
    }
}
