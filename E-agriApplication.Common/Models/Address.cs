using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Common.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? DoorNumber { get; set; }
        public string? Village { get; set; }
        public string? Landmark { get; set; }
        public string? Post { get; set; }
        public string? State { get; set; }
        public long PhoneNumber { get; set; }
        public string? District { get; set; }
        public long PINcodeNumber { get; set; }


    }
}
