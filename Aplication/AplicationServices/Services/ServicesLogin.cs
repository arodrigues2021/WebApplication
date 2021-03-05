using Aplication.AplicationServices.Interface;
using Aplication.Users;
using Aplication.Users.DTO;
using Domain.Users.DTO;
using Domain.Resources;
using System;
using Domain;

namespace Aplication.AplicationServices.Services
{
    public class ServicesLogin : ILogin
    {
        private readonly Config _config;

        public readonly IUsersService _UsersService;

        public ServicesLogin(IUsersService UsersService, Config config)
        {
            _UsersService = UsersService;
            _config = config;
        }

        public ResponseDto validarlogin(string email, string password)
        {
            LoginDTO pRet = _UsersService.GetUsersByEmail(email, password);
            var message = string.Empty;
            bool result = true;
            if (pRet != null)
            {
                //TimeSpan diascaduca = (pRet.FechaCreacion - System.DateTime.Now.Date);
                //if (DateTime.Now > pRet.FechaCreacion.AddDays(Convert.ToInt32(_config.appSettings.numdiascambio)))
                //{
                //    message = Messages.Caducada;
                //    result = false;
                //}
            }
            else
            {
                message = pRet == null ? Messages.Email : string.Empty;
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
    }
}
