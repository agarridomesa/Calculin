using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculinWeb.ViewModels;

namespace CalculinWebCore.Data
{
    public class ApplicationdbContext : IdentityDbContext<UsuarioConversor>
    {
        public ApplicationdbContext(DbContextOptions<ApplicationdbContext>options): base(options)
        {
        }
        public DbSet<CalculinWeb.ViewModels.HomeViewModel> HomeViewModel { get; set; }
    }
}
