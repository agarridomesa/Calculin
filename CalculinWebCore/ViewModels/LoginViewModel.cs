using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalculinWebCore.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string Nick { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool Rememberme { get; set; }
    }
}
