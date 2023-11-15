using EcommerceApplication.Common.Data;
using EcommerceApplication.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Repositories
{
    
        public interface IProductRepository
        {
        Task CreateProduct(Product product);
        Task<List<Product>> GetAllProduct();
        Task<Product> GetProductByName(string productName);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetAllProductByName(string productName);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);

    }
        public class ProductRepository : IProductRepository
        {
        public readonly IUnitOfWork _unitOfWork;
            public ProductRepository(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

            }
        public async Task CreateProduct(Product product)
        {
            _unitOfWork.Add(product);
           await _unitOfWork.CommitAsync();
        }
        public async Task<List<Product>> GetAllProduct() 
        {
          return await _unitOfWork.GetEntities<Product>().ToListAsync();
        }
        public async Task<Product> GetProductByName(string productName)
        {
            return await _unitOfWork.GetEntities<Product>().Where(x => x.Name == productName).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.GetEntities<Product>().Where(p=>p.Id == id).AsNoTracking().FirstOrDefaultAsync();
          
        }

        public async Task<List<Product>> GetAllProductByName(string productName)
        {
           
           return await _unitOfWork.GetEntities<Product>().Where(p => p.Name.Contains(productName)).ToListAsync();
        }
       public  async Task UpdateProduct(Product product)
        {
            _unitOfWork.Update(product);
           await _unitOfWork.CommitAsync();
        }
        public async Task DeleteProduct(Product product)
        {
           _unitOfWork.Remove(product);
           await _unitOfWork.CommitAsync();
           
        }


    }
}

