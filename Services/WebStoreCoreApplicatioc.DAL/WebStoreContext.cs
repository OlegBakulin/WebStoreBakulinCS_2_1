using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebStoreCoreApplicatioc.DAL
{
    public class WebStoreContext : IdentityDbContext<User>
    {
        public WebStoreContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
       
    }
}
