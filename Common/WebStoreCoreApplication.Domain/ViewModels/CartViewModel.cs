using System.Collections.Generic;
using System.Linq;

namespace WebStoreCoreApplication.Domain.ViewModels
{ 
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; }
        public int ItemsCount => Items?.Sum(x => x.Value) ?? 0;
    }
}