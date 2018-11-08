using System;
using Repositorio;
using Modelo;

namespace RepositorioFuncCore
{
    public interface IRepositorioFunc
    {
        double HacerConversion(double dinero, string siglas1, string siglas2);
        void Extraer(string symbol, int pos);
    }
    public class RepositorioFunc : IRepositorioFunc
    {
        private readonly IRepositorioMonedas _Repositorio;
        public RepositorioFunc(IRepositorioMonedas rep)
        {
            _Repositorio = rep;
        }

        public  double HacerConversion(double dinero, string siglas1, string siglas2)
        {
            var rep = _Repositorio;
            Moneda monedao = new Moneda();
            Moneda monedad = new Moneda();
            double fac;
            double resultado;
            monedao = rep.ReadMoneda(siglas1);
            monedad = rep.ReadMoneda(siglas2);
            fac = rep.ReadFactor(monedao, monedad);
            resultado = dinero * fac;
            return resultado;
        }
        public void Extraer(string symbol, int pos)
        {
            var rep = _Repositorio;
            var siglas = symbol.Substring(pos, 3);
            var moneda = new Moneda { Siglas = siglas, EnUso = true, Nombre = siglas };
            rep.InsertMoneda(moneda);
        }
    }
}
