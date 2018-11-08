using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace dbContext
{
    public class CalculinDB : DbContext
    {
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Conversión> Conversiones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Historial> Historiales { get; set; }

        public CalculinDB(DbContextOptions<CalculinDB> options) : base(options)
        {
        }

        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<CalculinDB>
        {
            public CalculinDB CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<CalculinDB>();
                optionsBuilder.UseSqlServer(
                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = CalculinDBCore; Integrated Security = True; MultipleActiveResultSets = True");

                return new CalculinDB(optionsBuilder.Options);
            }
        }
    }
}
