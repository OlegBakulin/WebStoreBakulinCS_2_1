using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreCoreApplication.Domain.ViewModels
{
    public class CartOrderViewModel
    {
        public CartViewModel Carting { get; set; }

        public OrderViewModel Order { get; set; } = new OrderViewModel();
    }
}
