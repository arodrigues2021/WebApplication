using Domain.Users.DTO;
using PresentationWeb.Models;

namespace Aplication.AplicationServices.Interface
{
    public interface ILogin
    {
        ResponseDto validarlogin(string email, string password);

        Usuario ConsultarBalanceginAsync(string email);
    }
}
