﻿using Aplication.Users;
using Aplication.Users.DTO;
using Domain.Users.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationWeb.Base;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class HomeController : BaseController
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        public HomeController(IUsersService UsersService):base(UsersService){}

        public IActionResult Index()
        {

            //Todo:Armando
            List<Usuario> pRet = _UsersService.GetListUsers();

            log.Info("HomeController-GetListUsers");

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
