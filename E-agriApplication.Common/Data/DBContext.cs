using Microsoft.EntityFrameworkCore;
using EcommerceApplication.Common.Models;

namespace EcommerceApplication.Common.Data
{
    public class assignmentDb: DbContext
    {
     
        public assignmentDb(DbContextOptions<assignmentDb> options):base (options) 
        { 
        }
    
        public DbSet<Users> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Address> addresses { get; set; }






    }
}
