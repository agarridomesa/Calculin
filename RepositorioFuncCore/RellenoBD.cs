using Modelo;
using Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RepositorioFuncCore
{
    public static class  RellenoBD
    {
        public static List<Pais> CrearListaPaises()
        {
            return ProcesarArchivo("Paises.csv");
        }

        public static List<Pais> ProcesarArchivo(string paisesCsv)
        {
            var query =

                File.ReadAllLines(paisesCsv)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToPais();

            return query.ToList();
            throw new NotImplementedException();
        }

        public static IEnumerable<Pais> ToPais(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Pais
                {
                    Nombre = columns[0],
                    Siglas = columns[1]

                };
            }
        }
    }
}

