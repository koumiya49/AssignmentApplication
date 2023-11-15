using EcommerceApplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Common.DTOs
{
    public class AddressViewModel
    {
        public int UserId { get; set; }
        public string? UserName { get;set; }
        public string? DoorNumber { get; set; }
        public string? Village { get; set; }
        public string? Post { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? LandMark { get; set; }
        public long PINcodeNumber { get; set; }
        public long PhoneNumber { get; set; }
    }
}
