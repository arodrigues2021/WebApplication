using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Aplication.Users.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellido requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Email requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Movil requerido")]
        public string Movil { get; set; }
        [Required(ErrorMessage = "Balance requerido")]
        public decimal Balance { get; set; }

    }
}
