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
        public ProductsClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.ProductsAdress) { }

        public IEnumerable<CategoryDTO> GetCategories() => Get<IEnumerable<CategoryDTO>>($"{_ServiceAddress}/sections");

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{_ServiceAddress}/brands");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            Post(_ServiceAddress, Filter ?? new ProductFilter())
               .Content
               .ReadAsAsync<IEnumerable<ProductDTO>>()
               .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_ServiceAddress}/{id}");
    }
}
