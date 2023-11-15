using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace EcommerceApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        public CartController(ICartServices cartServices)
        {
            this._cartServices = cartServices;
        }
        [HttpPost,Route("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId)
        {
          await _cartServices.AddToCart(productId);
           return StatusCode(200,"cart added");
           

        }
        [HttpDelete, Route("RemoveFromCartByProductName")]

        public async Task<IActionResult> RemoveFromCart(string productName)
        {
             var cart=await _cartServices.RemoveFromCart(productName);
           
                return Ok(cart);
           

        }
        [HttpDelete,Route("RemoveCartById")]

        public async Task<IActionResult> RemoveCartById(int cartId)
        {
           var cart= await _cartServices.RemoveCartByCartId(cartId);
            if (cart != null)
            {
                return Ok(cart);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet, Route("GetAllProductFromCart")]
        public async Task<IActionResult> GetAllProductFromCart()
        {
          var cartProduct = await _cartServices.GetAllProductFromCart();
            return Ok(cartProduct);

        }
        [HttpPut,Route("ChangeQuantity")]

        public async Task<IActionResult> ChangeQuantity(int CartId,int Quantity) 
        {
            var cart=await _cartServices.ChangeQuantity(CartId, Quantity);
            return Ok(cart);

        }
        [HttpGet,Route("GetCartItemByName")]
        public async Task<IActionResult> GetCartItemByName(string Name)
        {
            var cart=await _cartServices.GetCartByName(Name);
            if(cart != null)
            {
                return Ok("Not in cart");
            }
            else
            {
                return NoContent();
            }

        }
    }
}
