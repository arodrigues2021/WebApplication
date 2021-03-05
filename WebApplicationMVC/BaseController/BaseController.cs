using Aplication.AplicationServices.Interface;
using Aplication.Users;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace PresentationWeb.Base
{
    public class BaseController : Controller
    {
        public readonly IUsersService _UsersService;
        public readonly ILogin _ServicesLogin;
        public Config _config;


        public BaseController(IUsersService UsersService)
        {
            _UsersService = UsersService;
        }

        public BaseController(ILogin ServicesLogin)
        {
            _ServicesLogin = ServicesLogin;
        }

        public BaseController(ILogin ServicesLogin, IUsersService UsersService, Config config)
        {
            _ServicesLogin = ServicesLogin;
            _UsersService = UsersService;
            _config = config;
        }
    }

}
