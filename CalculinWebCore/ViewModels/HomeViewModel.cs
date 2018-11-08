using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculinWeb.ViewModels
{
    public class HomeViewModel
    {
        public List<Modelo.Moneda> ListaMonedas { get; set; }
        public string Titulo { get; set; }
        public string ImagenMoneda { get; set; }
        public string ID { get; set; }
    }
}
