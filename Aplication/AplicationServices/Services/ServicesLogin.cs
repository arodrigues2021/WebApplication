using Aplication.AplicationServices.Interface;
using Aplication.Users;
using Aplication.Users.DTO;
using Domain.Users.DTO;
using Domain.Resources;
using Domain;
using PresentationWeb.Models;
using System.Linq;
using Domain.DTO;

namespace Aplication.AplicationServices.Services
{
    public class ServicesLogin : ILogin
    {
        private readonly Config _config;

        private readonly apiContext _context;

        public readonly IUsersService _UsersService;
        public ServicesLogin(IUsersService UsersService, Config config, apiContext context)
        {
            _UsersService = UsersService;
            _config = config;
            _context = context;
        }

        public ResponseDto validarlogin(string email, string password)
        {
            LoginDTO pRet = _UsersService.GetUsersByEmail(email, password);
            var message = string.Empty;
            bool result = true;
            if (pRet == null)
            {
                message = Messages.Email;
                result = false;
            }
            ResponseDto response = new ResponseDto
            {
                data    = pRet,
                message = message,
                result  = result,
            };

            return response;
        }

        public Usuario ConsultarBalanceginAsync(string email)
        {
            var result = _context.Usuarios.Where(x => x.Email == email).FirstOrDefault();
            BalanceDTO balance = new BalanceDTO();
            balance.id = result.Id;
            balance.Nombre = result.Nombre;
            balance.Email = result.Email;
            balance.Balance = result.Balance.ToString();
            return result;

        }

       
    }
}
