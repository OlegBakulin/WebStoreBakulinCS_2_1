using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreCoreApplication.Controllers.Infrastructure.Interfaces;
using WebStoreCoreApplication.Controllers.Infrastructure.Services;
using WebStoreCoreApplication.ViewModels;
namespace WebStoreCoreApplication.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductServices _productServices;
        public BrandViewComponent(IProductServices productServices)
        {
            _productServices = productServices;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brand = GetBrands();
            return View(brand);
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var dbbrand = _productServices.GetBrands();
            return dbbrand.Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order,
                ProdCount = 0
            }).OrderBy(b => b.Order).ToList();
        }
    }
}
