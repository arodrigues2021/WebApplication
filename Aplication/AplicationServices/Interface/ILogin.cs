using Domain.Users.DTO;

namespace Aplication.AplicationServices.Interface
{
    public interface ILogin
    {
        ResponseDto validarlogin(string email, string password);
    }
}
