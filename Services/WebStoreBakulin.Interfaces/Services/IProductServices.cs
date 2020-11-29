using System.Collections.Generic;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IProductServices
    {
        IEnumerable<CategoryDTO> GetCategories();
        IEnumerable<BrandDTO> GetBrands();
        IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null);
        ProductDTO GetProductById(int id);
    }
}
