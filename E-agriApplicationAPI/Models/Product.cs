using System.ComponentModel.DataAnnotations;

namespace E_agriApplicationAPI.Models
{
    public class Product
    {
        [Key]
        public Guid Ids { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        //navigation property
        public Users users { get ; set; }
       


    }
}
