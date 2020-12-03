using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStoreCoreApplication.Domain.ViewModels;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface ICartService
    {
        void DecrementFromCart(int id);
        void RemoveFromCart(int id);
        void RemoveAll();
        void AddToCart(int id);

        CartViewModel TransformCart();
    }
}
