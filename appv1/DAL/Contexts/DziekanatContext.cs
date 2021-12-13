using Microsoft.EntityFrameworkCore;
using appv1.DAL.Models;

namespace appv1.DAL.Contexts
{
    public class DziekanatContext : DbContext
    {
        public DziekanatContext(DbContextOptions<DziekanatContext> options) : base(options)
        {

        }

        public DbSet<Zajecia> Zajecia { get; set; }
    }
}
