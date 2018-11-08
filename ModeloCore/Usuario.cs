using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public int IdPais { get; set; }
        public int IdHistorial { get; set; }
        public string Nick { get; set; }
        public string Nombre { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
