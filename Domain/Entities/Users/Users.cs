using System;

namespace Domain.Users.Entities
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Movil { get; set; }

        public decimal Balance { get; set; }
    }
}