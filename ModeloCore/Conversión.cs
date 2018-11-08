using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Conversión
    {
        [Key]
        public int Id { get; set; }
        public string Origensiglas { get; set; }
        public string Destinosiglas { get; set; }
        public Double Factorconversion { get; set; }
    }
}
