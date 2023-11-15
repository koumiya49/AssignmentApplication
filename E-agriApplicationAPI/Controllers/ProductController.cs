using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;
using EcommerceApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;

namespace EcommerceApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductController : Controller
    {
        private readonly IProductService _productServices;
        public ProductController(IProductService productService)
        {
            this._productServices = productService;
        }
        [HttpPost, Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            await _productServices.CreateProduct(productViewModel);
            return StatusCode(200,"Product Added successfully");
        }
        [HttpGet,Route("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
           var products= await _productServices.GetAllProduct();
            return StatusCode(200, products);
        }
        [HttpGet,Route("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var products =await _productServices.GetProductByName(productName);
            return StatusCode(200, products);
        }
        [HttpGet, Route("GetAllProductByName")]
        public async Task<IActionResult> GetAllProductByName(string productName)
        {
            var products = await _productServices.GetAllProductByName(productName);
            return StatusCode(200, products);
        }
        [HttpGet, Route("GetProductById")]
            public async Task<IActionResult> GetProductById(int Id)
            {
                var products =await _productServices.GetProductById(Id);
                return StatusCode(200, products);
            }
        
        [HttpDelete,Route("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(int Id)
        {
            await _productServices.DeleteProductById(Id);
            return StatusCode(200, "Deleted successfully");
        }
        [HttpDelete, Route("DeleteAllProductByName")]
        public async Task<IActionResult> DeleteProductByName(string productName)
        {
           await _productServices.DeleteProductByName(productName);
            return StatusCode(200, "Deleted successfully");
        }
        [HttpPut,Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductEditViewModel product)
        {
         await _productServices.EditProduct(product);
            return StatusCode(200, "Updated successfully");
        }
        [HttpGet,Route("GetStockByName")]
        public async Task<IActionResult> GetStockByName(string name)
        {
           var stock= await _productServices.GetStockByName(name);
            return StatusCode(200, stock);
        }
        [HttpPost, Route("AddImage")]
        public async Task<IActionResult> AddImage(IFormFile image, string productName)
        {
            var result = await _productServices.addandUpateimages(image, productName);
            return StatusCode(200, result);
        }
    }
}
