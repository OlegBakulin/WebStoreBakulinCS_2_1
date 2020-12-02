using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers
{
        public class CatalogController : Controller
    {
        private readonly IProductServices _productServices;

        public CatalogController(IProductServices productService)
        {
            _productServices = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product == null) return NotFound();
            /*
            Мой вариант дз 7
            return View(product);
            */

            return View(new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand?.Name ?? string.Empty
            });

        }

        public IActionResult Shop( int? brandId, int? categoryId)
        {
            var products = _productServices.GetProducts(new ProductFilter { BrandId = brandId, CategoryId = categoryId });

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(pr => new ProductViewModel()
                {
                    Id = pr.Id,
                    ImageUrl = pr.ImageUrl,
                    Name = pr.Name,
                    Order = pr.Order,
                    Price = pr.Price
                }).OrderBy(p => p.Order).ToList()
            };

            return View(model);
        }
    }
}
