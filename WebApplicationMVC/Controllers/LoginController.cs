
using PresentationWeb.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Users.DTO;
using System.Web.Http.Description;
using System.Collections.Generic;
using Aplication.AplicationServices.Interface;
using Aplication.Users.DTO;
using Aplication.Users;
using Domain.DTO;
using System;

namespace PresentationWeb.Controllers
{
    public class LoginController : BaseController
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoginController));

        public LoginController(ILogin ServicesLogin, IUsersService UsersService) : base(ServicesLogin, UsersService) { }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/user/validarloginAsync/{email}/{password}")]
        //Ejemplo:https://localhost:44341/api/validarloginAsync/arodrigues2010@hotmail.com/123
        public async Task<IActionResult> validarloginAsync(string email, string password)
        {
            try
            {
                return Ok(await Task.FromResult(_ServicesLogin.validarlogin(email, password)).ConfigureAwait(false));
            }
            catch (Exception err)
            {
                log.Error("LoginController-validarloginAsync");
                ResponseDto response = new ResponseDto
                {
                    data = "",
                    message = err.Message,
                    result = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Registra usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/Addusuario")]
        public async Task<IActionResult> Addusuario(UsuarioDTO usuario)
        {
            try
            {
                return Ok(await Task.FromResult(_UsersService.AddUsuario(usuario)).ConfigureAwait(false));
            }
            catch (Exception err)
            {
                log.Error("LoginController-Addusuario");
                ResponseDto response = new ResponseDto
                {
                    data = "",
                    message = err.Message,
                    result = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Borrar usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/BorrarUsuario")]
        public async Task<IActionResult> DeleteUsuario(BorradoDTO usuario)
        {
            try
            {
                if (usuario.Email == null)
                    return BadRequest("Parámetros Vacíos");

                return Ok(await Task.FromResult(_UsersService.BorrarUsuario(usuario)).ConfigureAwait(false));

            }
            catch (Exception err)
            {
                log.Error("LoginController-DeleteUsuario");
                ResponseDto response = new ResponseDto
                {
                    data = "",
                    message = err.Message,
                    result = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Update usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/UpdateUsuario")]
        public async Task<IActionResult> UpdateUsuario(UsuarioDTO usuario)
        {
            try
            {
                return Ok(await Task.FromResult(_UsersService.UpdateUsuario(usuario)).ConfigureAwait(false));
            }
            catch (Exception err)
            {
                log.Error("LoginController-UpdateUsuario");
                ResponseDto response = new ResponseDto
                {
                    data = "",
                    message = err.Message,
                    result = false
                };
                return BadRequest(response);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
