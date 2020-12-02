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
using WebStoreCoreApplication.Domain;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class SqlProductService : IProductServices
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _context.Categorys.AsEnumerable().Select(c => c.ToDTO());
        }

        public IEnumerable<BrandDTO> GetBrands()
        {
            return _context.Brands.AsEnumerable().Select(b => b.ToDTO());
        }

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
            return _context.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(x => x.Id == id).ToDTO();
        }

        
    }
}
