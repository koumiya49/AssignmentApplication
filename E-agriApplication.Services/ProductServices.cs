using Azure.Core;
using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Mappings;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Services
{
    public interface IProductService
    {

        Task CreateProduct(ProductViewModel productViewModel);
        Task<List<Product>> GetAllProduct();
        Task<List<Product>> GetAllProductByName(string productName);
        Task<Product> GetProductById(int Id);
        Task<Product> GetProductByName(string productName);
        Task DeleteProductById(int id);
        Task DeleteProductByName(string productName);
        Task UpdateProduct(ProductViewModel product);
        Task<int> GetStockByName(string productName);
        Task<bool> addandUpateimages(IFormFile file, string productName);
        Task EditProduct(ProductEditViewModel product);

    }
    public class ProductServices: IProductService
    {
        public readonly IProductRepository _productRepository;
        public readonly IProductMapper _mapper;
        public readonly ICartRepository _cartRepository;
        public ProductServices(IProductRepository productRepository, IProductMapper mapper, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }
        public async Task CreateProduct(ProductViewModel productViewModel)
        {
            //var image=convertImage(productViewModel.Image);
            var product = _mapper.MapProductViewModelToProject(productViewModel,null);
           await _productRepository.CreateProduct(product);
        }
        public async  Task<List<Product>> GetAllProduct()
        {
            var products=await _productRepository.GetAllProduct();

            return products;
        }
        public async Task<List<Product>> GetAllProductByName(string productName)
        {
            var products =await _productRepository.GetAllProductByName(productName);
            return products;
        }
        public async Task<Product> GetProductById(int Id)
        {
            var products = await _productRepository.GetProductById(Id);
            return products;

        }
        public async Task<Product> GetProductByName(string productName)
        {
            var products =await _productRepository.GetProductByName(productName);
            return products;
        }

       public async Task DeleteProductById(int id)
        {
            var product = await _productRepository.GetProductById(id);
            var cart = await _cartRepository.GetCartByProductName(product.Name);
            if (cart != null)
            {
                await _cartRepository.Remove(cart);
            }
            await _productRepository.DeleteProduct(product);
           
        }
        public  async Task DeleteProductByName(string productName)
        {
            var product = await _productRepository.GetProductByName(productName);
            await _productRepository.DeleteProduct(product);
            
            
        }
       public async Task UpdateProduct(ProductViewModel product)
        {
            var image=convertImage(product.Image);
            var products = await _productRepository.GetProductByName(product.Name);
            products.Name = product.Name;
            products.Price = product.Price;
            products.Stock = product.Stock;
            products.Description = product.Description;
            products.Image = image;

           await _productRepository.UpdateProduct(products);
        }
        public async Task< int> GetStockByName(string productName)
        {
            var product= await _productRepository.GetAllProductByName(productName);
            int stock = 0;
            foreach (var item in product)
            {
                    stock += item.Stock;   
            }
           
            return stock;
        }
        public string convertImage(IFormFile image)
        {
           
            using (var memoryStream = new MemoryStream())
            {
                image.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
                
            }
           
        }
        public async Task<bool> addandUpateimages(IFormFile file,string productName)
        {
            var product = await GetProductByName(productName);
            var productimage = new ProductViewModel()
            {
                Description= product.Description,
                Image=file,
                Name= product.Name,
                Price= product.Price,
                Stock= product.Stock,
            };

            await UpdateProduct(productimage);

            return true;
        }
        public async Task EditProduct(ProductEditViewModel product)
        {
            var products = await _productRepository.GetProductById(product.id);
            products.Name = product.Name;
            products.Price = product.Price;
            products.Stock = product.Stock;
            products.Description = product.Description;
            products.Image=products.Image;
            await _productRepository.UpdateProduct(products);
        }
        
    }
}
