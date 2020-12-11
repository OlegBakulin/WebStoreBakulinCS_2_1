using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStoreBakulin.Clients.Base;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.DTO.Order;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrdersService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, WebApiAdress.Orders) { }

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{_ServiceAddress}/{id}");

        
        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
             var response = await PostAsync($"{_ServiceAddress}/{userName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();}

    }
}
