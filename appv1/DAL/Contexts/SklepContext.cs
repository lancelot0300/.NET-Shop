using Microsoft.EntityFrameworkCore;
using appv1.DAL.Models;

namespace appv1.DAL.Contexts
{
    public class SklepContext : DbContext
    {
        public SklepContext(DbContextOptions<SklepContext> options) : base(options)
        {

        }


    public DbSet<Products> Products { get; set; }
    }
}
