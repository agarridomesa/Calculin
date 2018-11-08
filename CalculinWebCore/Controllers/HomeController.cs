using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CalculinWebCore.Models;
using CalculinWeb.ViewModels;
using Repositorio;
using CalculinWebCore.ViewModels;
using RepositorioFuncCore;
using ForexQuotes;
using Modelo;
using CalculinWebCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CalculinWebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorioMonedas _Repositorio;
        private readonly IRepositorioFunc _RepositorioFunc;
        private readonly SignInManager<UsuarioConversor> signInManager;
        private readonly UserManager<UsuarioConversor> userManager;
        public HomeController(IRepositorioMonedas repositorio, IRepositorioFunc repositoriofunc, UserManager<UsuarioConversor> userManager, SignInManager<UsuarioConversor> signInManager)
        {
            _Repositorio = repositorio;
            _RepositorioFunc = repositoriofunc;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        private Task<UsuarioConversor> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        #region // Ventanas generales
        public IActionResult Index()
        {
            
            return View();
        }
        
        public IActionResult About()
        {
            var client = new ForexDataClient("iq5IFnq426n3QufDY24SOxMOoDTQJftO");
            var symbols = client.GetSymbols();
            foreach (var symbol in symbols)
            {
                _RepositorioFunc.Extraer(symbol, 0);
                _RepositorioFunc.Extraer(symbol, 3);
            }

            var factores = client.GetQuotes(client.GetSymbols());
            foreach (var q in factores)
            {
                Conversión conversor = new Conversión();
                conversor.Origensiglas = q.symbol.Substring(0, 3);
                conversor.Destinosiglas = q.symbol.Substring(3, 3);
                conversor.Factorconversion = q.price;
                _Repositorio.InsertFactor(conversor);
            }
            _Repositorio.InsertPaises(RellenoBD.CrearListaPaises());
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult VerMonedas()
        {
            var listaMonedas = _Repositorio.ReadAllMonedas();

            var homeViewModel = new HomeViewModel
            {
                Titulo = "Calculin",
                ListaMonedas = listaMonedas,
                ImagenMoneda = "https://fotografias-viajestic.atresmedia.com/clipping/cmsimages02/2016/04/24/137E7EBB-01D1-42A4-A776-57AFBE4F228F/58.jpg"
            };

            return View(homeViewModel);
        }

        public IActionResult Details (int id)
        {
            var moneda = _Repositorio.ReadMonedaid(id);
            var detailsViewModel = new DetailsViewModel
            {
                Moneda = moneda,
                Id = id
            };
                
            if (detailsViewModel == null)
                return NotFound();

            return View(detailsViewModel);
        }

        [HttpPost]
        public IActionResult Details(DetailsViewModel details)
        {
            Moneda monedaupdate = new Moneda() { Id = details.Id, Nombre = details.Moneda.Nombre, Siglas = details.Moneda.Siglas, EnUso = details.Moneda.EnUso };
            _Repositorio.UpdateMoneda(monedaupdate);
            var moneda = _Repositorio.ReadMonedaid(details.Id);
            var detailsViewModel = new DetailsViewModel
            {
                Moneda = moneda,
                Id = details.Id
            };

            if (detailsViewModel == null)
                return NotFound();

            return View(detailsViewModel);
        }
        #endregion
        #region // Ventanas de usuario
        [Authorize]
        public async Task<IActionResult> Conversor()
        {
            var user = await GetCurrentUserAsync();
            var listaMonedas = _Repositorio.ReadAllMonedas();
            var listahistorial = _Repositorio.Ultimos10registros(user.UserName);
            var conversorviewmodel = new ConversorViewModel
            {
                Titulo = "Calculin",
                ListaMonedas = listaMonedas,
                ListaHistorial = listahistorial
            };

            return View(conversorviewmodel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Conversor(ConversorViewModel conversor)
        {
            var listaMonedas = _Repositorio.ReadAllMonedas();
            var resultado = _RepositorioFunc.HacerConversion(conversor.Cifra, conversor.MonedasO, conversor.MonedasD);
            Moneda monedao = new Moneda()
            {
               Siglas = conversor.MonedasO
            };
            Moneda monedad = new Moneda()
            {
                Siglas = conversor.MonedasD
            };
            var factor = _Repositorio.ReadFactor(monedao, monedad);
            var user = await GetCurrentUserAsync();
            Historial historial = new Historial()
            {
                NickUsuario = user.UserName,
                SMO = conversor.MonedasO,
                SMD = conversor.MonedasD,
                Fecha = DateTime.Now,
                Cantidad = conversor.Cifra,
                FactorConversion = factor,
                Resutado = resultado
            };
            _Repositorio.InsertHistorial(historial);
            var listahistorial = _Repositorio.Ultimos10registros(user.UserName);
            var conversorviewmodel = new ConversorViewModel
            {
                Titulo = "Calculin",
                ListaMonedas = listaMonedas,
                Resultado = resultado,
                ListaHistorial = listahistorial
            };

            return View(conversorviewmodel);
        }
        #endregion
    }
}
