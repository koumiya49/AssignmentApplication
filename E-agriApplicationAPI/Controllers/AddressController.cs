using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;
        public AddressController(IAddressServices addressServices)
        {
            _addressServices=addressServices;
        }

        [HttpPost, Route("CreateAddress")]
        public async Task<IActionResult> CreateAddress(AddressViewModel address)
        {
            await _addressServices.CreateAddress(address);
            return StatusCode(200, "Address Added Successfully");


        }
    
 
        [HttpDelete, Route("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            await _addressServices.DeleteAddress(id);
            return StatusCode(200, "Address Removed");


        }
        [HttpGet, Route("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            var address = await _addressServices.GetAllAddresses();
            return StatusCode(200, address);

        }
        [HttpGet,Route("GetAddressById")]

        public async Task<IActionResult> GetAddressById(int UserId)
        {
            var address=await _addressServices.GetAddressesById(UserId);
            return StatusCode(200, address);
        }
        [HttpPut, Route("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress(Address address)
        {
            await _addressServices.UpdateAddress(address);
            return StatusCode(200, "Address Updated ");

        }
    }
}

