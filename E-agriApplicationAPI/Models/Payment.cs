using System.ComponentModel.DataAnnotations;

namespace E_agriApplicationAPI.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public decimal Total { get; set; }
    }
}
