using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.Services;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class SqlProductService : IProductServices
    {
        private readonly WebStoreContext _context;

        public SqlProductService(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categorys.ToList();
        }

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var contextProducts = _context.Products.Include(p => p.Category).Include(p => p.Brand).AsQueryable();
            if (filter.BrandId.HasValue)
                contextProducts.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                contextProducts = contextProducts.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));

            return contextProducts.ToList();
        }

        public Product GetProductById(int id)
        {
            //Мой Вариант ДЗ 7
            //var product = _context.Products.Include("Category").Include("Brand");
            //return product;
            return _context.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(x => x.Id == id);
        }
    }
}
