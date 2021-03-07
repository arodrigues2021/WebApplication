using Aplication.Users;
using Aplication.Users.DTO;
using Domain.Users.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationWeb.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : BaseController
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public HomeController(IUsersService UsersService):base(UsersService){}

       
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
