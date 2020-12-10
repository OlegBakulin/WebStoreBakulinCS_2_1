using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Mapping;
using WebStoreCoreApplication.Domain.DTO.Products;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class SqlProductService : IProductServices
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context) => _context = context;

        public IEnumerable<CategoryDTO> GetCategories() => _context.Categorys.AsEnumerable().Select(c => c.ToDTO());

        public CategoryDTO GetCategoryById(int id) => _context.Categorys.Find(id).ToDTO();

        public IEnumerable<BrandDTO> GetBrands() => _context.Brands.Include(b => b.Products).AsEnumerable().Select(b => b.ToDTO());

        public BrandDTO GetBrandById(int id) => _context.Brands.Include(b => b.Products).FirstOrDefault(b => b.Id == id).ToDTO();


        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null)
        {
            IQueryable<Product> query = _context.Products
               .Include(product => product.Brand)
               .Include(product => product.Category);
            {
                if (filter?.Ids?.Length > 0)
                    query = query.Where(product => filter.Ids.Contains(product.Id));
                else
                {
                    if (filter?.BrandId != null)
                        query = query.Where(product => product.BrandId == filter.BrandId);

                    if (filter?.CategoryId != null)
                        query = query.Where(product => product.CategoryId == filter.CategoryId);
                }
                return query.AsEnumerable().ToDTO();
            }
        }

        ProductDTO IProductServices.GetProductById(int id)
        {
            return _context.Products                
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefault(x => x.Id == id)
                .ToDTO();
        }

        
    }
}
