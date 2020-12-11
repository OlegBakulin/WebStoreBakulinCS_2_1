using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Mapping;

namespace WebStoreCoreApplication.ViewComponents
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductServices _ProductData;

        public BreadCrumbsViewComponent(IProductServices ProductData) => _ProductData = ProductData;


        public IViewComponentResult Invoke()
        {
            var model = new BreadCrumbsViewModel();

            if (int.TryParse(Request.Query["CategoryId"], out var category_id))
            {
                model.Category = _ProductData.GetCategoryById(category_id).FromDTO();
                if (model.Category.ParentId != null)
                    model.Category.ParentCategory = _ProductData.GetCategoryById((int)model.Category.ParentId).FromDTO();
            }

            if (int.TryParse(Request.Query["BrandId"], out var brand_id))
                model.Brand = _ProductData.GetBrandById(brand_id).FromDTO();

            if (int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out var product_id))
            {
                var product = _ProductData.GetProductById(product_id);
                if (product is not null)
                    model.Product = product.Name;
            }

            return View(model);
        }
    }
}
