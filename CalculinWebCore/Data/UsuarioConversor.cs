using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculinWebCore.Data
{
    public class UsuarioConversor : IdentityUser
    {
        public DateTime FechaNacimiento { get; set; }
        public int IdPais { get; set; }
    }
}
