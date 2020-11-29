﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreBakulin.Interfaces.Services;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Services.Products
{
    public class CookieCartService : ICartService
    {
        private readonly IProductServices _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;
        private void ReplaceCookies(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_cartName);
            cookies.Append(_cartName, cookie);
        }

        private Cart Cart
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cart_cookies = context.Request.Cookies[_cartName];
                if (cart_cookies is null)
                {
                    var cart = new Cart();
                    cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookies(cookies, cart_cookies);
                return JsonConvert.DeserializeObject<Cart>(cart_cookies);
            }
            set => ReplaceCookies(_httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }

        public CookieCartService(IProductServices productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

            _cartName = "cart_"
                        + (_httpContextAccessor
                            .HttpContext.User.Identity.IsAuthenticated
                            ? _httpContextAccessor.HttpContext.User.Identity.Name
                            : "");
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item == null)
                return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveAll()
        {
            Cart.Items.Clear();
        }

        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });

            Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _productService.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(i => i.ProductId).ToArray()
            }).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                Brand = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = Cart.Items.Select(
                    x => (products[x.ProductId],
                    x.Quantity))
            };

            return r;
        }
    }
}
