using Microsoft.EntityFrameworkCore;
using E_agriApplicationAPI.Models;

namespace E_agriApplicationAPI.Data
{
    public class EAgriDBContext: DbContext
    {
     
        public EAgriDBContext(DbContextOptions<EAgriDBContext> options):base (options) 
        { 
        }
    
        public DbSet<Users> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Payment> payments { get; set; }



    }
}
