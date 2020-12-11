using System.Collections.Generic;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IProductServices
    {
        IEnumerable<CategoryDTO> GetCategories();
        CategoryDTO GetCategoryById(int id);
        IEnumerable<BrandDTO> GetBrands();
        BrandDTO GetBrandById(int id);

        IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null);
        ProductDTO GetProductById(int id);
        
    }
}
