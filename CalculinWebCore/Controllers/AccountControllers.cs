using CalculinWebCore.Data;
using CalculinWebCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculinWebCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UsuarioConversor> userManager;
        private readonly SignInManager<UsuarioConversor> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepositorioMonedas _Repositorio;
        public AccountController(IRepositorioMonedas repositorio, UserManager<UsuarioConversor> userManager, SignInManager<UsuarioConversor> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            _Repositorio = repositorio;
        }
        #region // Ventanas de usuario
        [HttpGet]
        public IActionResult Register()
        {
            List<Pais> paises = _Repositorio.ReadAllPaises();
            var registerviewmodel = new RegisterViewModel
            {
                Paises = paises,
                Password = "",
                ConfirmPassword = "",
                Nick = "",
                Email = ""
            };
            return View(registerviewmodel);
        }

        [HttpPost]
        public async Task<IActionResult>  Register(RegisterViewModel registro)
        {
            //if (!ModelState.IsValid)
            //    return RedirectToAction("Register", "Account");
            List<Pais> paises = _Repositorio.ReadAllPaises();
            var User = new UsuarioConversor()
            {
                Email = registro.Email,
                UserName = registro.Nick,
                FechaNacimiento = registro.FechaNacimiento,
                IdPais = registro.IdPais
            };
            var result = await userManager.CreateAsync(User, registro.Password);

            if (result.Succeeded)
            {
                return View("Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error", error.Description);
            }

            var registerviewmodel = new RegisterViewModel
            {
                Paises = paises
            };

            return View(registerviewmodel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login, string returnUrl = null)
        {

            var result = await signInManager.PasswordSignInAsync(login.Nick, login.Password, login.Rememberme, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            return View();
        }
        #endregion
        #region //funciones extra
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "home");
        }
        #endregion
    }
}
