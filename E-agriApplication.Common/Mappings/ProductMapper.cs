using EcommerceApplication.Common.DTOs;
using EcommerceApplication.Common.Models;

namespace EcommerceApplication.Common.Mappings
{
    public interface IProductMapper
    {
        Product MapProductViewModelToProject(ProductViewModel productViewModel, string image);

    }

    public class ProductMapper : IProductMapper
    {
        public Product MapProductViewModelToProject(ProductViewModel productViewModel, string image)
        {
            return new Product
            {
                Name = productViewModel.Name,
                Price = productViewModel.Price,
                Stock = productViewModel.Stock,
                Description = productViewModel.Description,
                Image = image,
            };
        }
    }
}
