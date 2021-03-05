using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO
{
    public class BorradoDTO
    {
        [Required(ErrorMessage = "Email requerido")]
        public string Email { get; set; }

    }
}
