using System.ComponentModel.DataAnnotations;

namespace EcommerceApplication.Common.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public long  PhoneNumber { get; set; }    
         

      
    }
}
