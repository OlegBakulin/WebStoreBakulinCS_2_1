using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.DTO.Order;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrders(string userName);
        Task<OrderDTO> GetOrderById(int id);
        Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel);
    }
}
