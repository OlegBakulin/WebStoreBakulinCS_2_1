using System.Collections.Generic;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IProductServices
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Brand> GetBrands();
        IEnumerable<Product> GetProducts(ProductFilter filter);
        Product GetProductById(int id);
    }
}
