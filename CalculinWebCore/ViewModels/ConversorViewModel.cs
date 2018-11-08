using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculinWebCore.ViewModels
{
    public class ConversorViewModel
    {
        public List<Modelo.Moneda> ListaMonedas { get; set; }
        public string Titulo { get; set; }
        public string MonedasO { get; set; }
        public string MonedasD { get; set; }
        public double Cifra { get; set; }
        public double Resultado { get; set; }
        public List<Modelo.Historial> ListaHistorial { get; set; }
    }
}
