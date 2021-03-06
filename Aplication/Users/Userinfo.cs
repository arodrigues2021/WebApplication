using System.ComponentModel.DataAnnotations;

namespace Aplication.AplicationServices.Services
{
    public class Userinfo
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
