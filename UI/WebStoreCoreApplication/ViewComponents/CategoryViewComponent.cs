using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
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

        public IViewComponentResult Invoke(string CategoryID)
        {
            var category_id = int.TryParse(CategoryID, out var id) ? id : (int?)null;

            var category = GetCategories(category_id, out var parent_category_id);

            return View(new SelectCategotyViewModel
            {
                Category = category,
                CurrentCategoryId = category_id,
                ParentCategoryId = parent_category_id
            });
        }

        /*
public async Task<IViewComponentResult> InvokeAsync()
{
   var Category = GetCategories();
   return View(Category);
}
*/

        private IEnumerable<CategoryViewModel> GetCategories(int? CategoryId, out int? ParentCategoryId)
        {
            ParentCategoryId = null;
            var categories = _productservice.GetCategories();
            var parentcategory = categories.Where(p => p.ParentId is null);
            var parentcategoryview = parentcategory
                .Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order
            })
               .ToList();
            /*
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
            */
            foreach (var CatViewModel in parentcategoryview)
            {
                var childcategory = categories.Where(c => c.ParentId == CatViewModel.Id);
                foreach (var childcat in childcategory)
                {
                    if (childcat.Id == CategoryId)
                        ParentCategoryId = childcat.ParentId;
                    CatViewModel.ChildCategory.Add(new CategoryViewModel
                    {
                        Id = childcat.Id,
                        Name = childcat.Name,
                        Order = childcat.Order,
                        ParentCategory = CatViewModel
                    });
                }
                CatViewModel.ChildCategory = CatViewModel.ChildCategory.OrderBy(c => c.Order).ToList();
            }
            parentcategory = parentcategory.OrderBy(c => c.Order).ToList();
            return parentcategoryview;
        }
    }
}
