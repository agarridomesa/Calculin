using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Moneda
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Siglas { get; set; }
        public bool EnUso { get; set; }
    }
}
