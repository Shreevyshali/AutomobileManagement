using automobileCar.Models;
using Microsoft.EntityFrameworkCore;

namespace automobileCar.Data
{
    public class Applicationdbcontext : DbContext
    {
        public Applicationdbcontext(DbContextOptions<Applicationdbcontext> options) : base(options)
        { 
        
        }
        public DbSet<Brand> Brand {  get; set; }
    }
}
