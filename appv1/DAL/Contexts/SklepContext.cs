using Microsoft.EntityFrameworkCore;
using appv1.DAL.Models;
using appv1.Models;

namespace appv1.DAL.Contexts
{
    public class SklepContext : DbContext
    {
        public SklepContext(DbContextOptions<SklepContext> options) : base(options)
        {

        }


        public DbSet<Products> Products { get; set; }

        public DbSet<Login> Login { get; set; }
        public DbSet<KoszykDoBazy> Koszyk { get; set; }

        public DbSet<Zamowienie> Zamowienia { get; set; }
    }
}
