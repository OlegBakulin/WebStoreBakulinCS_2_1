using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStoreCoreApplicatioc.DAL;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreCoreApplication.Domain.DTO.Order;
using WebStoreBakulin.Services.Mapping;
using WebStoreCoreApplication.Domain.Entities.Identity;

namespace WebStoreCoreApplication.Controllers.Infrastructure.Services
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string userName)
    {
            return await _context
                .Orders
                .Include(x => x.User)
                .Include(x => x.OrderItems)
                .Where(x => x.User.UserName == userName)
                .Select(x => x.ToDTO())
                .ToArrayAsync();
        }

        public async Task<OrderDTO> GetOrderById(int id) => (await _context.Orders
               .Include(order => order.User)
               .Include(order => order.OrderItems)
               .FirstOrDefaultAsync(order => order.Id == id))
           .ToDTO();
        /*Task<OrderDTO> IOrdersService.GetOrderById(int id)
        {
            var ord = _context
.Orders
.Include(order => order.User)
           .Include(order => order.OrderItems)
           .Where(order => order.User.UserName == UserName)
           .ToArrayAsync())
           .Select(order => order.ToDTO());
            ////
            .Include("OrderItems")
            .Include("User")
            .FirstOrDefault(x => x.Id == id);
            ////
            return new OrderDTO
            {
                Id = ord.Id,
                Name = ord.Name,
                Phone = ord.Phone,
                Address = ord.Address,
                Date = ord.Date,
                Items = ord.OrderItems.Select
            };
        }
*/
        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null) throw new InvalidOperationException($"Пользователь {UserName} на найден");

            await using var transaction = await _context.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user,
                Date = DateTime.Now
            };

            foreach (var item in OrderModel.Items)
            {
                var product = await _context.Products.FindAsync(item.Id);
                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price, // здесь может быть применена скидка
                    Quantity = item.Id,
                    Product = product
                };
                order.OrderItems.Add(order_item);
            }

            await _context.Orders.AddAsync(order);
            //await _db.OrderItems.AddRangeAsync(order.Items); // излишняя операция - элементы заказа и так попадут в БД
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDTO();
        }
    }
}
