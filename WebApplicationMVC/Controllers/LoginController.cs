
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

namespace PresentationWeb.Controllers
{
    public class LoginController : BaseController
    {

        public LoginController(ILogin ServicesLogin, IUsersService UsersService) : base(ServicesLogin, UsersService) { }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/user/validarloginAsync/{email}/{password}")]
        //Ejemplo:https://localhost:44341/api/validarloginAsync/arodrigues2010@hotmail.com/123
        public async Task<IActionResult> validarloginAsync(string email, string password)
        {
            return Ok(await Task.FromResult(_ServicesLogin.validarlogin(email, password)).ConfigureAwait(false));
        }

        /// <summary>
        /// Registra usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/Addusuario")]
        public async Task<IActionResult> Addusuario(UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Parámetros Vacíos");

            return Ok(await Task.FromResult(_UsersService.AddUsuario(usuario)).ConfigureAwait(false)); ;
        }

        /// <summary>
        /// Dorrar usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/BorrarUsuario")]
        public async Task<IActionResult> DeleteUsuario(BorradoDTO usuario)
        {
            if (usuario.Email == null)
                return BadRequest("Parámetros Vacíos");

            return Ok(await Task.FromResult(_UsersService.BorrarUsuario(usuario)).ConfigureAwait(false)); ;
        }

        /// <summary>
        /// Update usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(IEnumerable<ResponseDto>)), Route("api/users/UpadateUsuario")]
        public async Task<IActionResult> UpadateUsuario(UsuarioDTO usuario)
        {
            if (usuario == null)
                return BadRequest("Parámetros Vacíos");

            return Ok(await Task.FromResult(_UsersService.UpdateUsuario(usuario)).ConfigureAwait(false)); ;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
