
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
using System.Data.SqlClient;
using Domain;
using Microsoft.Extensions.Configuration;
using PresentationWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PresentationWeb.Controllers
{
    public class LoginController : BaseController
    {


        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(LoginController));
        public IConfiguration Configuration { get; set; }

        public Config config;
        public LoginController(ILogin ServicesLogin, 
            IUsersService UsersService, 
            Config config , 
            IUserInfoService UserInfoService,
            apiContext context) : base(ServicesLogin, UsersService, config, UserInfoService, context) { }
        
        /// <summary>
        /// Authenticate Token JWT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationModel model)
        {
            var user = _UserInfoService.Autenticate(model.Email,model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        /// <summary>
        /// Lista de Usuarios
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/user/GetListUsers")]
        public async Task<IActionResult> GetListUsers()
        {
            try
            {
                return Ok(await Task.FromResult(_UsersService.GetListUsers()).ConfigureAwait(false));
            }
            catch (Exception err)
            {
                log.Error("LoginController-GetListUsers");
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
        /// Validar si existe email y password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
                if (String.IsNullOrEmpty(usuario.Email))
                {
                    ResponseDto response = new ResponseDto
                    {
                        data = "",
                        message = "El email no puede estar en blanco",
                        result = false
                    };
                    return BadRequest(response);
                }

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


        /// <summary>
        /// Consultar balance de Usuario
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/user/ConsultarBalanceginAsync/{email}")]
        public async Task<IActionResult> ConsultarBalanceginAsync(string email)
        {
            try
            {
                return Ok(await Task.FromResult(_ServicesLogin.ConsultarBalanceginAsync(email)).ConfigureAwait(false));
            }
            catch (Exception err)
            {
                log.Error("LoginController-ConsultarBalanceginAsync");
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

        [Microsoft.AspNetCore.Mvc.ApiExplorerSettings(IgnoreApi = true)]
        [Route("working")]
        public ActionResult Working()
        {

            var connection = new SqlConnection(_config.connectionStrings.Database);

            if (String.IsNullOrEmpty(connection.ConnectionString))
            {
                ResponseDto response = new ResponseDto
                {
                    message = "Error ConnectionString esta vacia.",
                };
                return BadRequest(response);
            }
            else
            {
                try
                {
                    connection.Open();
                    ResponseDto response = new ResponseDto
                    {
                        message = "Base de Datos Ok."
                    };
                    connection.Close();
                    return BadRequest(response);
                }
                catch (SqlException)
                {
                    ResponseDto response = new ResponseDto
                    {
                        message = "Error al conectar Base de Datos"
                    };
                    return BadRequest(response);
                }
            }
        }
    }
}
