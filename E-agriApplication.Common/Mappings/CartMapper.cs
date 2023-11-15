using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Common.Mappings
{
    public interface ICartMapper
    {
        Cart MapFromCartViewToCart(Product carts);
    }
    public class CartMapper:ICartMapper
    {
        private readonly int Quantity=1;

        public Cart MapFromCartViewToCart(Product product)
        {
            return new Cart
            {
                ProductName = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = Convert.ToDecimal(product.Price),
                Quantity = 1,
                Total = Convert.ToDecimal(product.Price * Quantity)
            };  
        }
    }
}
