using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using dbContext;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Repositorio
{
    public interface IRepositorioMonedas
    {
        Moneda ReadMoneda(string sigla);
        Moneda ReadMonedaid(int id);
        List<Moneda> ReadAllMonedas();
        void InsertMoneda(Moneda coin);
        void InsertMonedas(List<Moneda> coins);
        void UpdateMoneda(Moneda coin);
        void DarAltaMoneda(Moneda coin);
        void DeleteMoneda(Moneda coin);
        double ReadFactor(Moneda Origensiglas, Moneda Destinosiglas);
        Conversión ReadConversion(Moneda Origensiglas, Moneda Destinosiglas);
        void InsertFactor(Conversión factor);
        void InsertFactores(List<Conversión> Factores);
        void UpdateFactor(Conversión factor);
        Usuario ReadUsuarioByNick(string nick);
        Usuario ReadUsuario(string email);
        void InsertUsuario(Usuario usuario);
        void InsertUsuarios(List<Usuario> usuarios);
        void UpdateUsuario(Usuario user);
        Pais ReadPais(string nombre);
        List<Pais> ReadAllPaises();
        void InsertPais(Pais pais);
        void InsertPaises(List<Pais> paises);
        void UpdatePais(Pais pais);
        Historial ReadHistorial(DateTime fecha);
        List<Historial> Ultimos10registros(string nombre);
        void InsertHistorial(Historial historial);
        void UpdateHistorial(Historial historial);
    }
    public class RepositorioMonedas : IRepositorioMonedas
    {
        private readonly CalculinDB _contexto;
        public RepositorioMonedas(CalculinDB contexto)
        {
            _contexto = contexto;
        }
        #region //Operaciones monedas-------------------------------------------------------------------
        public Moneda ReadMoneda(string sigla)
        {
            //using (var db = _contexto)
            //{
                Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Siglas == sigla);
                if (moneda != null)
                {
                    if (moneda.EnUso == false)
                    {
                        moneda = null;
                    }
                }

                return moneda;
            //}
        }
        public List<Moneda> ReadAllMonedas()
        {
            //using (var db = _contexto)
            //{
            var monedas = _contexto.Monedas;
            List<Moneda> todasmonedas = monedas.Where(a => a.EnUso == true).ToList();

            return todasmonedas;
            //}
        }
        public Moneda ReadMonedaid(int id)
        {
            //using (var db = _contexto)
            //{
                Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Id == id);
                if (moneda != null)
                {
                    if (moneda.EnUso == false)
                    {
                        moneda = null;
                    }
                }
                return moneda;
            //}
        }
        public void InsertMoneda(Moneda coin)
        {
            //using (var db = _contexto)
            //{
                var moneda = ReadMoneda(coin.Siglas);
                if (moneda == null)
                {
                    _contexto.Monedas.Add(coin);
                    _contexto.SaveChanges();
                }
                else
                {
                    UpdateMonedabysigla(coin);
                }
            //}
        }
        public void InsertMonedas(List<Moneda> coins)
        {
            //using (var db = _contexto)
            //{
                foreach (Moneda coin in coins)
                {
                    InsertMoneda(coin);
                }
            //}
        }
        public void UpdateMoneda(Moneda coin)
        {
            //using (var db = _contexto)
            //{
            Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Id == coin.Id);
            if (coin.Nombre != null)
            {
                moneda.Nombre = coin.Nombre;
            }
            if (coin.Siglas != null)
            {
                moneda.Siglas = coin.Siglas;
            }
            _contexto.SaveChanges();
            //}
        }
        public void UpdateMonedabysigla(Moneda coin)
        {
            //using (var db = _contexto)
            //{
            Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Siglas == coin.Siglas);
            if(coin.Nombre != null)
            {
                moneda.Nombre = coin.Nombre;
            }
            if (coin.Siglas != null)
            {
                moneda.Siglas = coin.Siglas;
            }
            _contexto.SaveChanges();
            //}
        }
        public void DarAltaMoneda(Moneda coin)
        {
            //using (var db = _contexto)
            //{
            Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Id == coin.Id);
            moneda.EnUso = true;
            _contexto.SaveChanges();
            //}
        }
        public void DeleteMoneda(Moneda coin)
        {
            //using (var db = _contexto)
            //{
                Moneda moneda = _contexto.Monedas.FirstOrDefault(a => a.Id == coin.Id);
                moneda.EnUso = false;
                _contexto.SaveChanges();
            //}
        }
#endregion
        #region //Operaciones factores------------------------------------------------------------------
        public double ReadFactor(Moneda Origensiglas, Moneda Destinosiglas)
        {
            //using (var db = _contexto)
            //{
                Conversión factor = _contexto.Conversiones.FirstOrDefault(a => a.Origensiglas == Origensiglas.Siglas && a.Destinosiglas == Destinosiglas.Siglas);
                double f = factor.Factorconversion;
                return f;
            //}
        }
        public Conversión ReadConversion(Moneda Origensiglas, Moneda Destinosiglas)
        {
            //using (var db = _contexto)
            //{
            Conversión factor = _contexto.Conversiones.FirstOrDefault(a => a.Origensiglas == Origensiglas.Siglas && a.Destinosiglas == Destinosiglas.Siglas);
            return factor;
            //}
        }
        public void InsertFactor(Conversión factor)
        {
            //using (var db = _contexto)
            //{
                Moneda origenmoneda = new Moneda();
                Moneda destinomoneda = new Moneda();
                origenmoneda.Siglas = factor.Origensiglas;
                destinomoneda.Siglas = factor.Destinosiglas;
                var iffactor = ReadConversion(origenmoneda, destinomoneda);
                if (iffactor == null)
                {
                    _contexto.Conversiones.Add(factor);
                    _contexto.SaveChanges();
                }
                else
                {
                    factor.Id = iffactor.Id;
                    UpdateFactor(factor);
                }
            //}
        }
        public void InsertFactores(List<Conversión> Factores)
        {
            //using (var db = _contexto)
            //{
                foreach (Conversión factor in Factores)
                {
                    InsertFactor(factor);
                }
             //}
        }
        public void UpdateFactor(Conversión factor)
        {
            //using (var db = _contexto)
            //{
                Conversión conversion = _contexto.Conversiones.FirstOrDefault(a => a.Id == factor.Id);
            if (conversion != null)
            {
                if(factor.Origensiglas != null)
                {
                    conversion.Origensiglas = factor.Origensiglas;
                }
                if (factor.Destinosiglas != null)
                {
                    conversion.Destinosiglas = factor.Destinosiglas;
                }
                if (factor.Factorconversion != 0)
                {
                    conversion.Factorconversion = factor.Factorconversion;
                }
                _contexto.SaveChanges();
            }
            //}
        }
        #endregion
        #region        //Operaciones usuarios------------------------------------------------------------------
        public Usuario ReadUsuarioByNick(string nick)
        {
            //using (var db = _contexto)
            //{
                Usuario usuario = _contexto.Usuarios.FirstOrDefault(a => a.Nick == nick);
                return usuario;
            //}
        }
        public Usuario ReadUsuario(string email)
        {
            //using (var db = _contexto)
            //{
                Usuario usuario = _contexto.Usuarios.FirstOrDefault(a => a.Email == email);
                return usuario;
            //}
        }
        public void InsertUsuario(Usuario usuario)
        {
            //using (var db = _contexto)
            //{
                var ifusuario = ReadUsuario(usuario.Email);
                if (ifusuario == null)
                {
                    _contexto.Usuarios.Add(usuario);
                    _contexto.SaveChanges();
                }
                else
                {
                    usuario.Id = ifusuario.Id;
                    UpdateUsuario(usuario);
                }
            //}
        }
        public void InsertUsuarios(List<Usuario> usuarios)
        {
            //using (var db = _contexto)
            //{
                foreach (Usuario usuario in usuarios)
                {
                    InsertUsuario(usuario);
                }
            //}
        }
        public void UpdateUsuario(Usuario user)
        {
            //using (var db = _contexto)
            //{
                Usuario usuario = _contexto.Usuarios.FirstOrDefault(a => a.Id == user.Id);
            if (usuario != null)
            {
                if (user.IdPais != 0)
                {
                    usuario.IdPais = user.IdPais;
                }
                if (user.IdHistorial != 0)
                {
                    usuario.IdHistorial = user.IdHistorial;
                }
                if (user.Nick != null)
                {
                    usuario.Nick = user.Nick;
                }
                if (user.Nombre != null)
                {
                    usuario.Nombre = user.Nombre;
                }
                if (user.Pass != null)
                {
                    usuario.Pass = user.Pass;
                }
                if (user.Email != null)
                {
                    usuario.Email = user.Email;
                }
                if (user.FechaNacimiento != null)
                {
                    usuario.FechaNacimiento = user.FechaNacimiento;
                }
                _contexto.SaveChanges();
            }
            //}
        }
        #endregion
        #region //Operaciones paises--------------------------------------------------------------------
        public Pais ReadPais(string nombre)
        {
            //using (var db = _contexto)
            //{
                Pais pais = _contexto.Paises.FirstOrDefault(a => a.Nombre == nombre);
                return pais;
            //}

        }
        public List<Pais> ReadAllPaises()
        {
            //using (var db = _contexto)
            //{
            var paises = _contexto.Paises;
            List<Pais> todospaises = paises.ToList();

            return todospaises;
            //}
        }
        public void InsertPais(Pais pais)
        {
            //using (var db = _contexto)
            //{
                var ifpais = ReadPais(pais.Nombre);
                if (ifpais == null)
                {
                    _contexto.Paises.Add(pais);
                    _contexto.SaveChanges();
                }
                else
                {
                    pais.Id = ifpais.Id;
                    UpdatePais(pais);
                }
            //}
        }
        public void InsertPaises(List<Pais> paises)
        {
            //using (var db = _contexto)
            //{
                foreach (Pais pais in paises)
                {
                    InsertPais(pais);
                }
            //}
        }
        public void UpdatePais(Pais pais)
        {
            //using (var db = _contexto)
            //{
            Pais nacion = _contexto.Paises.FirstOrDefault(a => a.Id == pais.Id);
            if (nacion != null)
            {
                if (pais.Siglas != null)
                {
                    nacion.Siglas = pais.Siglas;
                }
                if (pais.Nombre != null)
                {
                    nacion.Nombre = pais.Nombre;
                }
                _contexto.SaveChanges();
            }
            //}
        }
        #endregion
        #region        //Operaciones historial-----------------------------------------------------------------
        public Historial ReadHistorial(DateTime fecha)
        {
            //using (var db = _contexto)
            //{
                Historial historial = _contexto.Historiales.FirstOrDefault(a => a.Fecha == fecha);
                return historial;
            //}
        }
        public List<Historial> Ultimos10registros(string nombre)
        {
            //using (var db = _contexto)
            //{
                var historial = _contexto.Historiales.Where(a => a.NickUsuario == nombre).OrderByDescending(a => a.Fecha).Take(10).ToList();
                return historial;
            //}
        }
        public void InsertHistorial(Historial historial)
        {
            //using (var db = _contexto)
            //{
                _contexto.Historiales.Add(historial);
                _contexto.SaveChanges();
            //}
        }
        public void UpdateHistorial(Historial historial)
        {
            //using (var db = _contexto)
            //{
            Historial histo = _contexto.Historiales.FirstOrDefault(a => a.Id == historial.Id);
            if (histo != null)
            {
                if (historial.NickUsuario != null)
                {
                    histo.NickUsuario = historial.NickUsuario;
                }
                if (historial.Fecha != null)
                {
                    histo.Fecha = historial.Fecha;
                }
                if (historial.Cantidad != 0)
                {
                    histo.Cantidad = historial.Cantidad;
                }
                if (historial.FactorConversion != 0)
                {
                    histo.FactorConversion = historial.FactorConversion;
                }
                if (historial.Resutado != 0)
                {
                    histo.Resutado = historial.Resutado;
                }
                _contexto.SaveChanges();
            }
            //}
        }
        #endregion
    }
}
