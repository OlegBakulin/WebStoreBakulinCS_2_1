using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.Services;

namespace WebStoreBakulin.ServiceHosting.Controllers
{
    [Route(WebApiAdress.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductServices 
    {
        private readonly IProductServices _ProductData;

        public ProductsApiController(IProductServices ProductData) => _ProductData = ProductData;

        [HttpGet("category")] // http://localhost:5001/api/products/category
        public IEnumerable<CategoryDTO> GetCategories() => _ProductData.GetCategories();

        [HttpGet("brands")] // http://localhost:5001/api/products/brands
        public IEnumerable<BrandDTO> GetBrands() => _ProductData.GetBrands();

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter = null) =>
            _ProductData.GetProducts(Filter ?? new ProductFilter());

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);
    }
}

