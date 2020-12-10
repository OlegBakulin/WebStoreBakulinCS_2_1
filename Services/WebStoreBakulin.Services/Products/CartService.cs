using System.Linq;
using WebStoreCoreApplication.Domain;
using WebStoreCoreApplication.Domain.Entities;
using WebStoreCoreApplication.Domain.ViewModels;
using WebStoreBakulin.Interfaces.Services;
using WebStoreBakulin.Services.Mapping;

namespace WebStoreBakulin.Services.Products
{
    public class CartService : ICartService
    {
        private readonly IProductServices _ProductData;
        private readonly ICartStore _CartStore;

        public CartService(IProductServices ProductData, ICartStore CartStore)
        {
            _ProductData = ProductData;
            _CartStore = CartStore;
        }

        public void AddToCart(int id)
        {
            var cart = _CartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem {ProductId = id, Quantity = 1});
            else
                item.Quantity++;

            _CartStore.Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _CartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if(item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            _CartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _CartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            cart.Items.Remove(item);

            _CartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = _CartStore.Cart;

            cart.Items.Clear();

            _CartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids = _CartStore.Cart.Items.Select(item => item.ProductId).ToArray()
            });

            var products_view_models = products.FromDTO().ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = _CartStore.Cart.Items.Select(item => (products_view_models[item.ProductId], item.Quantity))
            };
        }

    }
}
