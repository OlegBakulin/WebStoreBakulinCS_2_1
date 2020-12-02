using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.Entities.Identity;

namespace WebStoreCoreApplicatioc.DAL
{
    public class WebStoreContext : IdentityDbContext<User, Role, string>
    {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public WebStoreContext(DbContextOptions<WebStoreContext> Options) : base(Options) { }
    }
}
