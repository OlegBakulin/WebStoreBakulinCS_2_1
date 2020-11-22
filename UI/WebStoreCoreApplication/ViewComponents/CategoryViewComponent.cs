using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Controllers.Infrastructure.Interfaces;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IProductServices _productservice;
        public CategoryViewComponent(IProductServices productServices)
        {
            _productservice = productServices;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Category = GetCategories();
            return View(Category);
        }

        private List<CategoryViewModel> GetCategories()
        {
            var categories = _productservice.GetCategories();
            var parentsection = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var parentcategory = new List<CategoryViewModel>();
            foreach (var parcat in parentsection)
            {
                parentcategory.Add(new CategoryViewModel()
                {
                    Id = parcat.Id,
                    Name = parcat.Name,
                    Order = parcat.Order,
                    ParentCategory = null
                });
            }

            foreach (var CatViewModel in parentcategory)
            {
                var childcategory = categories.Where(c => c.ParentId == CatViewModel.Id);
                foreach (var childcat in childcategory)
                {
                    CatViewModel.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childcat.Id,
                        Name = childcat.Name,
                        Order = childcat.Order,
                        ParentCategory = CatViewModel
                    });
                }
                CatViewModel.ChildCategories = CatViewModel.ChildCategories.OrderBy(c => c.Order).ToList();
            }
            parentcategory = parentcategory.OrderBy(c => c.Order).ToList();
            return parentcategory;
        }
    }
}
