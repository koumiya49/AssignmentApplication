using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Mappings;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Services
{
    public interface ICartServices
    {
        Task AddToCart(int product);
        Task<List<Cart>> RemoveFromCart(string productName);
        Task<List<Cart>> GetAllProductFromCart();
        Task<List<Cart>> ChangeQuantity(int CartId,int quantity);
        Task<List<Cart>> RemoveCartByCartId(int cartId);
        Task<Cart> GetCartByName(string Name);

    }
    public class CartServices : ICartServices
    {
        public readonly ICartRepository _cartRepository;
        public readonly ICartMapper _mapper;
        public readonly IProductRepository _productRepository;

        public CartServices(ICartRepository cartRepository, ICartMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;

        }
        public async Task AddToCart(int product)
        {
            var products = await _productRepository.GetProductById(product);
            var existingcart = await _cartRepository.GetCartByProductName(products.Name);
            if (existingcart == null) {
                var carts = _mapper.MapFromCartViewToCart(products);
                await _cartRepository.Add(carts);
            }
            else
            {
               
                await ChangeQuantity(existingcart.Id,existingcart.Quantity+1);
            }
            

        }
        public async Task<List<Cart>> RemoveFromCart(string productName)
        {
            var cart = await _cartRepository.GetCartByProductName(productName);
                await _cartRepository.Remove(cart);
            return await _cartRepository.GetAllCartProduct();


        }
        public async Task<List<Cart>> RemoveCartByCartId(int cartId)
        {
            var cartItem = await _cartRepository.GetCartByCartId(cartId);
            await _cartRepository.Remove(cartItem);
            return await _cartRepository.GetAllCartProduct();
        }
        public async Task<List<Cart>> GetAllProductFromCart()
        {
            return await _cartRepository.GetAllCartProduct();

        }
        public async Task<List<Cart>> ChangeQuantity(int CartId, int quantity)
        {
            var cartItem = await _cartRepository.GetCartByCartId(CartId);
            cartItem.Quantity = quantity;
            cartItem.Total = quantity * cartItem.Price;
            await _cartRepository.UpdateCartItem(cartItem);
            return await _cartRepository.GetAllCartProduct();

        }
        public async Task<Cart> GetCartByName(string Name)
        {
            var cartItem=await _cartRepository.GetCartByProductName(Name);
            return cartItem;
        }

    }
}
