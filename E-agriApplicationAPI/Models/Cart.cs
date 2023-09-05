using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace E_agriApplicationAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        // navigation property

        public Product products { get; set; }

    }
}
