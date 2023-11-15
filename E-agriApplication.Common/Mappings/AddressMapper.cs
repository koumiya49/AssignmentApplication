using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Common.Mappings
{
    public interface IAddressMapper
    {
        Address MapToAddress(AddressViewModel address);
    }
    public  class AddressMapper:IAddressMapper
    {
        public  Address MapToAddress(AddressViewModel address)
        {
            return new Address
            {
                UserId=address.UserId,
                 Village=address.Village,
                 Post=address.Post,
                 State=address.State,
                 Landmark=address.LandMark,
                District=address.District,
                DoorNumber=address.DoorNumber,
                FullName= address.UserName,
                PhoneNumber=address.PhoneNumber,
                PINcodeNumber=address.PINcodeNumber,

            };
      
    }



}
}
