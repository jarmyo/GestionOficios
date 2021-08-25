using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
#nullable disable

namespace GestionWeb.Data
{
    public partial class NucleoCompartidoContext : IdentityDbContext<IdentityUser>
    {
       

        public NucleoCompartidoContext(DbContextOptions<NucleoCompartidoContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

    public partial class GestionOficiosContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {                
                optionsBuilder.UseSqlServer(Core.ConnectionString);
            }
        }
    }
}