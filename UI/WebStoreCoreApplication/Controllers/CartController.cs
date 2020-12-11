using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.DTO.Order;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreCoreApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;
        private readonly IProductServices _ProductData;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;

        public CartController(ICartService cartService, IOrdersService ordersService)
        {
            _cartService = cartService;
            _ordersService = ordersService;
        }
        //AddToCart int id
        public IActionResult Details()
        {
            var model = new OrderDetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };

            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction(nameof(Details));
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);
            //return RedirectToAction(nameof(Details));
            return Redirect($"/ProductDetails/{id}");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel OrderModel, [FromServices] IOrdersService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel
                {
                    Carting = _cartService.TransformCart(),
                    Order = OrderModel
                });

            var create_order_model = new CreateOrderModel
            {
                Order = OrderModel,
                Items = _cartService.TransformCart().Items
                   .Select(item => new OrderItemDTO
                   {
                       Id = item.Product.Id,
                       Price = item.Product.Price,
                       Quantity = item.Quantity
                   })
                   .ToList()
            };

            var order = await OrderService.CreateOrder(User.Identity.Name, create_order_model);

            _cartService.RemoveAll();

            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
   
}
