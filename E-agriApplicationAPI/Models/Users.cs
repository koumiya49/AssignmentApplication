using System.ComponentModel.DataAnnotations;

namespace E_agriApplicationAPI.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
      
         

      
    }
}
