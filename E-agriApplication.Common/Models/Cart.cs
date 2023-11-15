using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace EcommerceApplication.Common.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; } 
        public string Description { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
       
    }
}
