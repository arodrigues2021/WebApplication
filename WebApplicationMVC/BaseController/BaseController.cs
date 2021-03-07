using Aplication.AplicationServices.Interface;
using Aplication.Users;
using Domain;
using Microsoft.AspNetCore.Mvc;
using PresentationWeb.Models;
using System.Net.Http;

namespace PresentationWeb.Base
{
    public class BaseController : Controller
    {
        public readonly IUsersService _UsersService;
        public readonly ILogin _ServicesLogin;
        public Config _config;
        public readonly IUserInfoService _UserInfoService;
        public readonly IHttpClientFactory _clientFactory;
        private readonly apiContext _context;

        public BaseController(IUsersService UsersService)
        {
            _UsersService = UsersService;
        }

        public BaseController(ILogin ServicesLogin)
        {
            _ServicesLogin = ServicesLogin;
        }

        public BaseController(ILogin ServicesLogin, 
            IUsersService UsersService, 
            Config config, 
            IUserInfoService UserInfoService,
            apiContext context
            )
        {
            _ServicesLogin = ServicesLogin;
            _UsersService = UsersService;
            _config = config;
            _UserInfoService = UserInfoService;
            _context = context;
        }
    }

}
