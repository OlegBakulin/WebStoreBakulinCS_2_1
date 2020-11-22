using System.Collections.Generic;

namespace WebStoreCoreApplication.ViewModels
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}