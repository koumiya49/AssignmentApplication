using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Mappings;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Services
{
public interface IAddressServices
{
        Task CreateAddress(AddressViewModel address);
        Task UpdateAddress(Address address);
        Task DeleteAddress(int id);
        Task<List<Address>> GetAllAddresses();
        Task<List<Address>> GetAddressesById(int Id);

}
      public class AddressServices: IAddressServices
      {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressMapper _mapper;

        public AddressServices(IAddressRepository addressRepository, IAddressMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
           

        }
        public async Task CreateAddress(AddressViewModel address)
        {
           
            var add = _mapper.MapToAddress(address);
            await _addressRepository.Add(add);


        }
        public async Task UpdateAddress(Address address)
        {
            var oldadd = await  _addressRepository.GetAddressById(address.Id);
            oldadd.PhoneNumber = address.PhoneNumber;
            oldadd.UserId = address.UserId;
            oldadd.Village = address.Village;
            oldadd.Post = address.Post;
            oldadd.State = address.State;
            oldadd.Landmark = address.Landmark;
            oldadd.District = address.District;
            oldadd.DoorNumber = address.DoorNumber;
            oldadd.FullName = address.FullName;
            oldadd.PhoneNumber = address.PhoneNumber;
            
            await _addressRepository.Update(oldadd);
        }
        
        public async Task DeleteAddress(int id) {
            var add= await _addressRepository.GetAddressById(id);
            
                await _addressRepository.Delete(add);
            
        }
        public async Task<List<Address>> GetAllAddresses()
        {
         return await _addressRepository.GetAll();

        }
        public async Task<List<Address>> GetAddressesById(int userId)
        {
            return await _addressRepository.GetById(userId);
        }


    }
}
