using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Repositories
{
    public interface ICartRepository
    {
        Task Add(Cart cart);
        Task Remove(Cart cart);
        Task<List<Cart>> GetAllCartProduct();
        Task<Cart> GetCartByProductName(string productName);
        Task<Cart> GetCartByCartId(int cartId);
        Task UpdateCartItem(Cart cart);
       
    }
   public class CartRepository: ICartRepository
    {
        public readonly IUnitOfWork _unitOfWork;
        public CartRepository(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;

        }
        public async Task Add(Cart cart)
        {
            _unitOfWork.Add(cart);
           await _unitOfWork.CommitAsync();
        }
       public  async Task Remove(Cart cart)
        {
            _unitOfWork.Remove(cart);
          await  _unitOfWork.CommitAsync();
        }
       public  async Task<List<Cart>> GetAllCartProduct()
        {
            return await _unitOfWork.GetEntities<Cart>().ToListAsync();
        }
        public async Task<Cart> GetCartByProductName(string productName)
        {
            return await _unitOfWork.GetEntities<Cart>().Where(cart => cart.ProductName == productName).AsNoTracking().FirstOrDefaultAsync();

        }
        public async Task<Cart> GetCartByCartId(int cartId)
        {
            return await _unitOfWork.GetEntities<Cart>().Where(cart => cart.Id == cartId).AsNoTracking().FirstOrDefaultAsync();

        }
        public async Task UpdateCartItem(Cart cart)
        {
           _unitOfWork.Update(cart);
            await _unitOfWork.CommitAsync();
        }

    }
}
