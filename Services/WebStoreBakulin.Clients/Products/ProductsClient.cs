using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStoreBakulin.Clients.Base;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Clients.Products
{
    public class ProductsClient : BaseClient, IProductServices
    {
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.Products) { }


        
        public IEnumerable<CategoryDTO> GetCategories() => Get<IEnumerable<CategoryDTO>>($"{_ServiceAddress}/sections");

        public CategoryDTO GetCategoryById(int id) => Get<CategoryDTO>($"{_ServiceAddress}/categorys/{id}");
        

        
        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{_ServiceAddress}/brands");

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{_ServiceAddress}/brands/{id}");
        
        
        
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            Post(_ServiceAddress, Filter ?? new ProductFilter())
               .Content
               .ReadAsAsync<IEnumerable<ProductDTO>>()
               .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_ServiceAddress}/{id}");

        

        
    }
}
