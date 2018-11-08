using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Historial
    {
        [Key]
        public int Id { get; set; }
        public string NickUsuario { get; set; }
        public string SMO { get; set; }
        public string SMD { get; set; }
        public DateTime Fecha { get; set; }
        public double Cantidad { get; set; }
        public double FactorConversion { get; set; }
        public double Resutado { get; set; }
    }
}
