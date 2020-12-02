using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Mapping;
using WebStoreCoreApplication.Controllers.Infrastructure.Services;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.ViewComponents
{
    public class BrandViewComponent : ViewComponent
    {
        private readonly IProductServices _ProductData;

        public BrandViewComponent(IProductServices ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke() => View(GetBrands());

        private IEnumerable<BrandViewModel> GetBrands() =>
            _ProductData.GetBrands()
               .Select(b => b.FromDTO())
               .Select(brand => new BrandViewModel
               {
                   Id = brand.Id,
                   Name = brand.Name,
                   Order = brand.Order
               })
               .OrderBy(brand => brand.Order);
    }
}