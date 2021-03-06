
using Aplication.AplicationServices.Services;


namespace Aplication.AplicationServices.Interface
{
    public interface IUserInfoService
    {
        Userinfo Autenticate(string usuario, string pasword);
    }
}
